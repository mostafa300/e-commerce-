namespace IA_E_commerce_.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_LN_Jobdescription_Photo2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "firstName", c => c.String());
            AddColumn("dbo.AspNetUsers", "phone", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "phone");
            DropColumn("dbo.AspNetUsers", "firstName");
        }
    }
}
