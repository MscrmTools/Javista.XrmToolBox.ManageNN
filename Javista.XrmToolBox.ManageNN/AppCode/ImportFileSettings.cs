namespace Javista.XrmToolBox.ManageNN.AppCode
{
    internal class ImportFileSettings
    {
        public decimal BatchCount { get; set; }
        public bool CacheFirstEntity { get; set; }
        public bool CacheSecondEntity { get; set; }
        public bool Debug { get; set; }
        public bool FirstAttributeIsGuid { get; set; }
        public string FirstAttributeName { get; set; }
        public string FirstEntity { get; set; }

        public bool ImportInBatch { get; set; }
        public string Relationship { get; set; }
        public bool SecondAttributeIsGuid { get; set; }
        public string SecondAttributeName { get; set; }
        public string SecondEntity { get; set; }
        public string Separator { get; internal set; }
    }
}