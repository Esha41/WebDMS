namespace DataMigration
{
    public  class DataMigrationConfiguraton
    {
        public int CompanyId { get; set; }
        public int RoleId { get; set; } 
        public int UserId { get; set; } 
        public string UserName { get; set; }
        public int CompanyCustomeFieldId { get; set; }
        public int CompanyCustomeFieldTypeId { get; set; }
        public int DocumentClassFieldTypeId { get; set; }
        public string DocumentClassFieldUiLabel { get; set; }
        public string CompanyCustomFieldUiLabel { get; set; }
        public string SourceServerName { get; set; }
        public string SourceDatabaseName { get; set; }
        public string SourceUserName { get; set; }
        public string SourcePassWord { get; set; }
        public string SourceBaseUrl { get; set; }
        //check validate database tables existence before execution or not
        public bool CheckSchema { get; set; }

    }
}
