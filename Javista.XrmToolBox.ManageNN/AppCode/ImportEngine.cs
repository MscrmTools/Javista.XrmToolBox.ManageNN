using Microsoft.Crm.Sdk.Messages;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Javista.XrmToolBox.ManageNN.AppCode
{
    public class ResultEventArgs : EventArgs
    {
        public string FirstValue;
        public int LineNumber;
        public string Message;
        public string SecondValue;
        public bool? Success;
    }

    internal class ImportEngine
    {
        private readonly string filePath;
        private readonly bool isDelete;
        private readonly IOrganizationService service;
        private readonly ImportFileSettings settings;
        private List<string[]> dataList;
        private List<string> lines;

        public ImportEngine(string filePath, IOrganizationService service, ImportFileSettings settings, bool isDelete = false)
        {
            this.filePath = filePath;
            this.service = service;
            this.settings = settings;
            this.isDelete = isDelete;
            lines = new List<string>();
            dataList = new List<string[]>();
        }

        public event EventHandler<ResultEventArgs> RaiseError;

        public event EventHandler<ResultEventArgs> RaiseSuccess;

        public event EventHandler<ResultEventArgs> SendInformation;

        public void Import(BackgroundWorker bw)
        {
            int processed = 0;

            var firstRecords = new List<Entity>();
            var secondRecords = new List<Entity>();
            var batch = new ExecuteMultipleRequest
            {
                Requests = new OrganizationRequestCollection(),
                Settings = new ExecuteMultipleSettings
                {
                    ContinueOnError = true,
                    ReturnResponses = true
                }
            };

            if (settings.Relationship == "systemuserroles_association"
                || settings.Relationship == "teamroles_association")
            {
                if (settings.FirstEntity == "role" && settings.FirstAttributeIsGuid
                    || settings.SecondEntity == "role" && settings.SecondAttributeIsGuid)
                {
                    string message = "To associate roles, you must use role name, not role unique identifier";
                    OnRaiseError(new ResultEventArgs { Message = message });
                    return;
                }

                ManageRoleAssociation();
                return;
            }

            if (settings.CacheFirstEntity)
            {
                SendInformation?.Invoke(this, new ResultEventArgs { Message = $"Caching info for entity '{settings.FirstEntity}'" });

                GetRecords(settings.FirstEntity, settings.FirstAttributeName, firstRecords);

                SendInformation?.Invoke(this, new ResultEventArgs { Message = $"{firstRecords.Count} recods cached" });
            }

            if (settings.CacheSecondEntity)
            {
                SendInformation?.Invoke(this, new ResultEventArgs { Message = $"Caching info for entity '{settings.SecondEntity}'" });

                GetRecords(settings.SecondEntity, settings.SecondAttributeName, secondRecords);

                SendInformation?.Invoke(this, new ResultEventArgs { Message = $"{secondRecords.Count} recods cached" });
            }

            int lineNumber = 0;
            foreach (var line in lines)
            {
                if (bw.CancellationPending)
                {
                    return;
                }
                lineNumber++;

                if (settings.Debug)
                {
                    SendInformation?.Invoke(this, new ResultEventArgs { Message = $"Processing line {lineNumber} ({line})" });
                }
                string[] data = new string[2];

                try
                {
                    if (!line.Contains(settings.Separator))
                    {
                        string message = $"The line does not contain the separator '{settings.Separator}'";
                        OnRaiseError(new ResultEventArgs { Success = false, LineNumber = lineNumber, Message = message });
                        continue;
                    }

                    using (TextFieldParser parser = new TextFieldParser(new StringReader(line))
                    {
                        HasFieldsEnclosedInQuotes = true
                    })
                    {
                        parser.SetDelimiters(settings.Separator);

                        while (!parser.EndOfData)
                        {
                            data = parser.ReadFields();
                        }
                    }

                    dataList.Add(data);

                    if (settings.Debug)
                    {
                        SendInformation?.Invoke(this, new ResultEventArgs { Message = $"First data: {data[0]}" });
                        SendInformation?.Invoke(this, new ResultEventArgs { Message = $"Second data: {data[1]}" });
                    }

                    Guid firstGuid, secondGuid;

                    if (settings.FirstAttributeIsGuid)
                    {
                        firstGuid = new Guid(data[0]);
                    }
                    else
                    {
                        firstGuid = firstRecords.FirstOrDefault(r =>
                                        r.GetAttributeValue<string>(settings.FirstAttributeName) == data[0])?.Id ??
                                    Guid.Empty;

                        if (firstGuid == Guid.Empty)
                        {
                            var records = service.RetrieveMultiple(new QueryExpression(settings.FirstEntity)
                            {
                                TopCount = 2,
                                Criteria =
                                    {
                                        Conditions =
                                        {
                                            new ConditionExpression(settings.FirstAttributeName,
                                                ConditionOperator.Equal,
                                                data[0])
                                        }
                                    }
                            });

                            if (records.Entities.Count == 1)
                            {
                                firstGuid = records.Entities.First().Id;
                            }
                            else if (records.Entities.Count > 1)
                            {
                                RaiseError(this,
                                    new ResultEventArgs
                                    {
                                        LineNumber = lineNumber,
                                        FirstValue = data[0],
                                        SecondValue = data[1],
                                        Success = false,
                                        Message =
                                            $"More than one record ({settings.FirstEntity}) were found with the value specified"
                                    });

                                continue;
                            }
                            else
                            {
                                RaiseError(this,
                                    new ResultEventArgs
                                    {
                                        FirstValue = data[0],
                                        SecondValue = data[1],
                                        Success = false,
                                        LineNumber = lineNumber,
                                        Message =
                                            $"No record ({settings.FirstEntity}) was found with the value specified"
                                    });

                                continue;
                            }
                        }
                    }

                    if (settings.SecondAttributeIsGuid)
                    {
                        secondGuid = new Guid(data[1]);
                    }
                    else
                    {
                        secondGuid = secondRecords.FirstOrDefault(r =>
                                        r.GetAttributeValue<string>(settings.SecondAttributeName) == data[0])?.Id ??
                                    Guid.Empty;

                        if (secondGuid == Guid.Empty)
                        {
                            var records = service.RetrieveMultiple(new QueryExpression(settings.SecondEntity)
                            {
                                TopCount = 2,
                                Criteria =
                                    {
                                        Conditions =
                                        {
                                            new ConditionExpression(settings.SecondAttributeName,
                                                ConditionOperator.Equal,
                                                data[1])
                                        }
                                    }
                            });

                            if (records.Entities.Count == 1)
                            {
                                secondGuid = records.Entities.First().Id;
                            }
                            else if (records.Entities.Count > 1)
                            {
                                RaiseError(this,
                                    new ResultEventArgs
                                    {
                                        LineNumber = lineNumber,
                                        FirstValue = data[0],
                                        SecondValue = data[1],
                                        Success = false,
                                        Message =
                                            $"More than one record ({settings.SecondEntity}) were found with the value specified"
                                    });

                                continue;
                            }
                            else
                            {
                                RaiseError(this,
                                    new ResultEventArgs
                                    {
                                        LineNumber = lineNumber,
                                        FirstValue = data[0],
                                        SecondValue = data[1],
                                        Success = false,
                                        Message =
                                            $"No record ({settings.SecondEntity}) was found with the value specified"
                                    });

                                continue;
                            }
                        }
                    }

                    if (settings.Relationship == "listcontact_association"
                        || settings.Relationship == "listaccount_association"
                        || settings.Relationship == "listlead_association")
                    {
                        var request = new AddListMembersListRequest
                        {
                            ListId = settings.FirstEntity == "list" ? firstGuid : secondGuid,
                            MemberIds = new[] { settings.FirstEntity == "list" ? secondGuid : firstGuid }
                        };

                        if (!settings.ImportInBatch)
                            service.Execute(request);
                        else
                        {
                            batch.Requests.Add(request);
                        }
                    }
                    else
                    {
                        OrganizationRequest request;

                        if (isDelete)
                        {
                            request = new DisassociateRequest
                            {
                                Target = new EntityReference(settings.FirstEntity, firstGuid),
                                Relationship = new Relationship(settings.Relationship),
                                RelatedEntities = new EntityReferenceCollection
                                {
                                    new EntityReference(settings.SecondEntity, secondGuid)
                                }
                            };

                            if (((DisassociateRequest)request).Target.LogicalName == ((DisassociateRequest)request).RelatedEntities.First().LogicalName)
                            {
                                ((DisassociateRequest)request).Relationship.PrimaryEntityRole = EntityRole.Referencing;
                            }
                        }
                        else
                        {
                            request = new AssociateRequest
                            {
                                Target = new EntityReference(settings.FirstEntity, firstGuid),
                                Relationship = new Relationship(settings.Relationship),
                                RelatedEntities = new EntityReferenceCollection
                                {
                                    new EntityReference(settings.SecondEntity, secondGuid)
                                }
                            };

                            if (((AssociateRequest)request).Target.LogicalName == ((AssociateRequest)request).RelatedEntities.First().LogicalName)
                            {
                                ((AssociateRequest)request).Relationship.PrimaryEntityRole = EntityRole.Referencing;
                            }
                        }

                        if (!settings.ImportInBatch)
                        {
                            service.Execute(request);
                            OnRaiseSuccess(new ResultEventArgs
                            {
                                LineNumber = lineNumber,
                                FirstValue = data[0],
                                SecondValue = data[1],
                                Success = true,
                            });
                        }
                        else
                        {
                            batch.Requests.Add(request);
                        }
                    }
                }
                catch (FaultException<OrganizationServiceFault> error)
                {
                    if (error.Detail.ErrorCode.ToString("X") == "80040237")
                    {
                        OnRaiseError(new ResultEventArgs
                        {
                            LineNumber = lineNumber,
                            FirstValue = data[0],
                            SecondValue = data[1],
                            Success = false,
                            Message = "Relationship was not created because it already exists"
                        });
                    }
                    else
                    {
                        OnRaiseError(new ResultEventArgs
                        {
                            LineNumber = lineNumber,
                            FirstValue = data[0],
                            SecondValue = data[1],
                            Success = false,
                            Message = error.Message
                        });
                    }
                }

                if (settings.ImportInBatch && batch.Requests.Count % settings.BatchCount == 0)
                {
                    var batchResponse = (ExecuteMultipleResponse)service.Execute(batch);
                    foreach (var response in batchResponse.Responses)
                    {
                        var totalLineNumber = processed + response.RequestIndex + 1;
                        if (response.Fault == null)
                        {
                            OnRaiseSuccess(new ResultEventArgs
                            {
                                LineNumber = totalLineNumber,
                                FirstValue = dataList[processed + response.RequestIndex][0],
                                SecondValue = dataList[processed + response.RequestIndex][1],
                                Success = true
                            });
                        }
                        else
                        {
                            if (response.Fault.ErrorCode.ToString("X") == "80040237")
                            {
                                OnRaiseError(new ResultEventArgs
                                {
                                    LineNumber = totalLineNumber,
                                    FirstValue = dataList[processed + response.RequestIndex][0],
                                    SecondValue = dataList[processed + response.RequestIndex][1],
                                    Success = false,
                                    Message = "Relationship was not created because it already exists"
                                });
                            }
                            else
                            {
                                OnRaiseError(new ResultEventArgs
                                {
                                    LineNumber = totalLineNumber,
                                    FirstValue = dataList[processed + response.RequestIndex][0],
                                    SecondValue = dataList[processed + response.RequestIndex][1],
                                    Success = false,
                                    Message = response.Fault.Message
                                });
                            }
                        }
                    }
                    processed += batch.Requests.Count;

                    batch.Requests.Clear();
                }
            }

            if (settings.ImportInBatch && batch.Requests.Count > 0)
            {
                var batchResponse = (ExecuteMultipleResponse)service.Execute(batch);
                foreach (var response in batchResponse.Responses)
                {
                    var totalLineNumber = processed + response.RequestIndex + 1;
                    if (response.Fault == null)
                    {
                        OnRaiseSuccess(new ResultEventArgs
                        {
                            LineNumber = totalLineNumber,
                            FirstValue = dataList[processed + response.RequestIndex][0],
                            SecondValue = dataList[processed + response.RequestIndex][1],
                            Success = true
                        });
                    }
                    else
                    {
                        if (response.Fault.ErrorCode.ToString("X") == "80040237")
                        {
                            OnRaiseError(new ResultEventArgs
                            {
                                LineNumber = totalLineNumber,
                                FirstValue = dataList[processed + response.RequestIndex][0],
                                SecondValue = dataList[processed + response.RequestIndex][1],
                                Success = false,
                                Message = "Relationship was not created because it already exists"
                            });
                        }
                        else
                        {
                            OnRaiseError(new ResultEventArgs
                            {
                                LineNumber = totalLineNumber,
                                FirstValue = dataList[processed + response.RequestIndex][0],
                                SecondValue = dataList[processed + response.RequestIndex][1],
                                Success = false,
                                Message = response.Fault.Message
                            });
                        }
                    }
                }

                batch.Requests.Clear();
            }
        }

        public int LoadFile()
        {
            if (!File.Exists(filePath))
            {
                SendInformation?.Invoke(this, new ResultEventArgs { Message = $"File '{filePath}' does not exist" });
            }
            SendInformation?.Invoke(this, new ResultEventArgs { Message = $"Separator is '{settings.Separator}'" });

            lines = new List<string>();

            using (var reader = new StreamReader(filePath, Encoding.Default))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            return lines.Count;
        }

        public void ManageRoleAssociation()
        {
            if (settings.ImportInBatch)
            {
                SendInformation?.Invoke(this, new ResultEventArgs { Message = "Batch processing not available for roles association" });
            }

            string[] data = new string[2];

            using (var reader = new StreamReader(filePath, Encoding.Default))
            {
                string line;
                int lineNumber = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    lineNumber++;

                    if (settings.Debug)
                    {
                        SendInformation?.Invoke(this, new ResultEventArgs { Message = $"Processing line {lineNumber} ({line})" });
                    }

                    try
                    {
                        if (!line.Contains(settings.Separator))
                        {
                            string message = $"The line does not contain the separator '{settings.Separator}'";
                            OnRaiseError(new ResultEventArgs { LineNumber = lineNumber, Message = message });
                            continue;
                        }

                        using (TextFieldParser parser = new TextFieldParser(new StringReader(line))
                        {
                            HasFieldsEnclosedInQuotes = true
                        })
                        {
                            parser.SetDelimiters(settings.Separator);

                            while (!parser.EndOfData)
                            {
                                data = parser.ReadFields();
                            }
                        }

                        if (settings.Debug)
                        {
                            SendInformation?.Invoke(this, new ResultEventArgs { Message = $"First data: {data[0]}" });
                            SendInformation?.Invoke(this, new ResultEventArgs { Message = $"Second data: {data[1]}" });
                        }

                        EntityReference buRef;
                        Entity principal;

                        if (settings.Relationship == "systemuserroles_association")
                        {
                            principal = service.RetrieveMultiple(new QueryExpression("systemuser")
                            {
                                ColumnSet = new ColumnSet("businessunitid"),
                                Criteria =
                                {
                                    Conditions =
                                    {
                                        new ConditionExpression(
                                            settings.FirstEntity == "systemuser"
                                                ? settings.FirstAttributeName
                                                : settings.SecondAttributeName, ConditionOperator.Equal,
                                            settings.FirstEntity == "systemuser" ? data[0] : data[1])
                                    }
                                }
                            }).Entities.FirstOrDefault();

                            if (principal == null)
                            {
                                OnRaiseError(new ResultEventArgs
                                {
                                    LineNumber = lineNumber,
                                    Success = false,
                                    FirstValue = data[0],
                                    SecondValue = data[1],
                                    Message =
                                        $"Unable to find a user with value {(settings.FirstEntity == "systemuser" ? data[0] : data[1])} for attribute {(settings.FirstEntity == "systemuser" ? settings.FirstAttributeName : settings.SecondAttributeName)}"
                                });
                                continue;
                            }

                            buRef = principal.GetAttributeValue<EntityReference>("businessunitid");
                        }
                        else
                        {
                            principal = service.RetrieveMultiple(new QueryExpression("team")
                            {
                                ColumnSet = new ColumnSet("businessunitid"),
                                Criteria =
                                {
                                    Conditions =
                                    {
                                        new ConditionExpression(
                                            settings.FirstEntity == "team"
                                                ? settings.FirstAttributeName
                                                : settings.SecondAttributeName, ConditionOperator.Equal,
                                            settings.FirstEntity == "team" ? data[0] : data[1])
                                    }
                                }
                            }).Entities.FirstOrDefault();

                            if (principal == null)
                            {
                                OnRaiseError(new ResultEventArgs
                                {
                                    LineNumber = lineNumber,
                                    Success = false,
                                    FirstValue = data[0],
                                    SecondValue = data[1],
                                    Message =
                                        $"Unable to find a team with value {(settings.FirstEntity == "team" ? data[0] : data[1])} for attribute {(settings.FirstEntity == "team" ? settings.FirstAttributeName : settings.SecondAttributeName)}"
                                });
                                continue;
                            }

                            buRef = principal.GetAttributeValue<EntityReference>("businessunitid");
                        }

                        var role = service.RetrieveMultiple(new QueryExpression("role")
                        {
                            Criteria =
                            {
                                Conditions =
                                {
                                    new ConditionExpression(settings.FirstEntity == "role"?settings.FirstAttributeName : settings.SecondAttributeName, ConditionOperator.Equal,
                                        settings.FirstEntity == "role"?data[0]:data[1]),
                                    new ConditionExpression("businessunitid", ConditionOperator.Equal, buRef.Id)
                                }
                            }
                        }).Entities.FirstOrDefault();

                        if (role == null)
                        {
                            OnRaiseError(new ResultEventArgs
                            {
                                LineNumber = lineNumber,
                                Success = false,
                                FirstValue = data[0],
                                SecondValue = data[1],
                                Message = $"Unable to find a role with value {(settings.FirstEntity == "role" ? data[0] : data[1])} for attribute {(settings.FirstEntity == "role" ? settings.FirstAttributeName : settings.SecondAttributeName)} and business unit {buRef.Name}"
                            });
                            continue;
                        }

                        if (isDelete)
                        {
                            var request = new DisassociateRequest
                            {
                                Target = new EntityReference(principal.LogicalName, principal.Id),
                                Relationship = new Relationship(settings.Relationship),
                                RelatedEntities = new EntityReferenceCollection
                                {
                                    new EntityReference(role.LogicalName, role.Id)
                                }
                            };

                            service.Execute(request);
                        }
                        else
                        {
                            var request = new AssociateRequest
                            {
                                Target = new EntityReference(principal.LogicalName, principal.Id),
                                Relationship = new Relationship(settings.Relationship),
                                RelatedEntities = new EntityReferenceCollection
                                {
                                    new EntityReference(role.LogicalName, role.Id)
                                }
                            };

                            service.Execute(request);
                        }

                        OnRaiseSuccess(new ResultEventArgs
                        {
                            LineNumber = lineNumber,
                            Success = true,
                            FirstValue = data[0],
                            SecondValue = data[1]
                        });
                    }
                    catch (FaultException<OrganizationServiceFault> error)
                    {
                        if (error.Detail.ErrorCode.ToString("X") == "80040237")
                        {
                            OnRaiseError(new ResultEventArgs
                            {
                                LineNumber = lineNumber,
                                Success = false,
                                FirstValue = data[0],
                                SecondValue = data[1],
                                Message = "Relationship was not created because it already exists"
                            });
                        }
                        else
                        {
                            OnRaiseError(new ResultEventArgs
                            {
                                LineNumber = lineNumber,
                                Success = false,
                                FirstValue = data[0],
                                SecondValue = data[1],
                                Message = error.Message
                            });
                        }
                    }
                }
            }
        }

        protected virtual void OnRaiseError(ResultEventArgs e)
        {
            EventHandler<ResultEventArgs> handler = RaiseError;

            handler?.Invoke(this, e);
        }

        protected virtual void OnRaiseSuccess(ResultEventArgs e)
        {
            EventHandler<ResultEventArgs> handler = RaiseSuccess;

            handler?.Invoke(this, e);
        }

        private void GetRecords(string entity, string attribute, List<Entity> records)
        {
            var query = new QueryExpression(entity)
            {
                NoLock = true,
                ColumnSet = new ColumnSet(attribute),
                PageInfo = new PagingInfo
                {
                    Count = 500,
                    PageNumber = 1
                }
            };

            EntityCollection ec;
            do
            {
                ec = service.RetrieveMultiple(query);
                records.AddRange(ec.Entities.ToArray());

                query.PageInfo.PageNumber++;
                query.PageInfo.PagingCookie = ec.PagingCookie;
            } while (ec.MoreRecords);
        }
    }
}