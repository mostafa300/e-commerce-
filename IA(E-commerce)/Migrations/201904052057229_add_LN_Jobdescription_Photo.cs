namespace IA_E_commerce_.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_LN_Jobdescription_Photo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "lastName", c => c.String());
            AddColumn("dbo.AspNetUsers", "jobDescription", c => c.String());
            AddColumn("dbo.AspNetUsers", "Photo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Photo");
            DropColumn("dbo.AspNetUsers", "jobDescription");
            DropColumn("dbo.AspNetUsers", "lastName");
        }
    }
}
