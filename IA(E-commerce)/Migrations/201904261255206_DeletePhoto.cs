namespace IA_E_commerce_.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletePhoto : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Photo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Photo", c => c.String());
        }
    }
}
