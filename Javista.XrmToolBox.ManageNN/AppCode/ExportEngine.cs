using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace Javista.XrmToolBox.ManageNN.AppCode
{
    public class ExportResultEventArgs : EventArgs
    {
        public string Message;
    }

    internal class ExportEngine
    {
        private readonly string filePath;
        private readonly IOrganizationService service;
        private readonly ImportFileSettings settings;

        public ExportEngine(string filePath, IOrganizationService service, ImportFileSettings settings)
        {
            this.filePath = filePath;
            this.service = service;
            this.settings = settings;
        }

        public event EventHandler<ExportResultEventArgs> RaiseError;

        public event EventHandler<ExportResultEventArgs> RaiseSuccess;

        public event EventHandler<ExportResultEventArgs> SendInformation;

        public void Export(BackgroundWorker bw)
        {
            var qe = new QueryExpression(settings.Relationship)
            {
                ColumnSet = new ColumnSet(true),
                PageInfo = new PagingInfo { Count = 250, PageNumber = 1 }
            };

            if (!settings.FirstAttributeIsGuid)
            {
                qe.LinkEntities.Add(new LinkEntity
                {
                    LinkFromEntityName = settings.Relationship,
                    LinkFromAttributeName = settings.FirstEntity + "id",
                    LinkToAttributeName = settings.FirstEntity + "id",
                    LinkToEntityName = settings.FirstEntity,
                    Columns = new ColumnSet(settings.FirstAttributeName),
                    EntityAlias = "first"
                });
            }

            if (!settings.SecondAttributeIsGuid)
            {
                qe.LinkEntities.Add(new LinkEntity
                {
                    LinkFromEntityName = settings.Relationship,
                    LinkFromAttributeName = settings.SecondEntity + "id",
                    LinkToAttributeName = settings.SecondEntity + "id",
                    LinkToEntityName = settings.SecondEntity,
                    Columns = new ColumnSet(settings.SecondAttributeName),
                    EntityAlias = "second"
                });
            }

            EntityCollection results;

            do
            {
                if (bw.CancellationPending)
                    return;

                results = service.RetrieveMultiple(qe);

                SendInformation?.Invoke(this, new ExportResultEventArgs { Message = $"Parsing page {qe.PageInfo.PageNumber}..." });

                using (var writer = new StreamWriter(filePath, true, Encoding.Default))
                {
                    foreach (var result in results.Entities)
                    {
                        try
                        {
                            string dataFirst;
                            string dataSecond;

                            Guid guidFirst, guidSecond;

                            if (settings.FirstEntity == "list" && (settings.SecondEntity == "contact"
                                                                   || settings.SecondEntity == "account"
                                                                   || settings.SecondEntity == "lead"))
                            {
                                guidFirst = result.GetAttributeValue<EntityReference>("listid").Id;
                                guidSecond = result.GetAttributeValue<EntityReference>("entityid").Id;
                            }
                            else if (settings.SecondEntity == "list" && (settings.FirstEntity == "contact"
                                                                         || settings.FirstEntity == "account"
                                                                         || settings.FirstEntity == "lead"))
                            {
                                guidFirst = result.GetAttributeValue<EntityReference>("entityid").Id;
                                guidSecond = result.GetAttributeValue<EntityReference>("listid").Id;
                            }
                            else if (settings.FirstEntity == settings.SecondEntity)
                            {
                                guidFirst = result.GetAttributeValue<Guid>(settings.FirstEntity + "idone");
                                guidSecond = result.GetAttributeValue<Guid>(settings.SecondEntity + "idtwo");
                            }
                            else
                            {
                                guidFirst = result.GetAttributeValue<Guid>(settings.FirstEntity + "id");
                                guidSecond = result.GetAttributeValue<Guid>(settings.SecondEntity + "id");
                            }

                            if (!settings.FirstAttributeIsGuid)
                            {
                                dataFirst = result.GetAttributeValue<AliasedValue>("first." + settings.FirstAttributeName)?.Value?.ToString();

                                //var record = service.Retrieve(settings.FirstEntity, guidFirst,
                                //    new ColumnSet(settings.FirstAttributeName));

                                //if (!record.Contains(settings.FirstAttributeName))
                                if (string.IsNullOrEmpty(dataFirst))
                                {
                                    OnRaiseError(new ExportResultEventArgs
                                    {
                                        Message =
                                            string.Format("The record '{0}' ({1}) does not contain value for attribute '{2}' and so the NN relationship cannot be exported",
                                            //record.Id.ToString("B"),
                                            result.GetAttributeValue<Guid>(settings.FirstEntity + "id"),
                                            settings.FirstEntity,
                                            settings.FirstAttributeName)
                                    });
                                    continue;
                                }
                                //dataFirst = record[settings.FirstAttributeName].ToString();
                            }
                            else
                            {
                                dataFirst = guidFirst.ToString("B");
                            }

                            if (!settings.SecondAttributeIsGuid)
                            {
                                dataSecond = result.GetAttributeValue<AliasedValue>("second." + settings.SecondAttributeName)?.Value?.ToString();

                                //var record = service.Retrieve(settings.SecondEntity, guidSecond,
                                //    new ColumnSet(settings.SecondAttributeName));

                                //if (!record.Contains(settings.SecondAttributeName))
                                if (string.IsNullOrEmpty(dataSecond))
                                {
                                    OnRaiseError(new ExportResultEventArgs
                                    {
                                        Message =
                                            string.Format("The record '{0}' ({1}) does not contain value for attribute '{2}' and so the NN relationship cannot be exported",
                                                //record.Id.ToString("B"),
                                                result.GetAttributeValue<Guid>(settings.SecondEntity + "id"),
                                            settings.SecondEntity,
                                            settings.SecondAttributeName)
                                    });
                                    continue;
                                }
                                //dataSecond = record[settings.SecondAttributeName].ToString();
                            }
                            else
                            {
                                dataSecond = guidSecond.ToString("B");
                            }

                            writer.WriteLine("{0}{1}{2}", dataFirst, settings.Separator, dataSecond);

                            OnRaiseSuccess(new ExportResultEventArgs());
                        }
                        catch (Exception error)
                        {
                            OnRaiseError(new ExportResultEventArgs
                            {
                                Message = error.Message
                            });
                        }
                    }
                }

                qe.PageInfo.PageNumber++;
                qe.PageInfo.PagingCookie = results.PagingCookie;
            } while (results.MoreRecords);
        }

        protected virtual void OnRaiseError(ExportResultEventArgs e)
        {
            EventHandler<ExportResultEventArgs> handler = RaiseError;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnRaiseSuccess(ExportResultEventArgs e)
        {
            EventHandler<ExportResultEventArgs> handler = RaiseSuccess;

            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}