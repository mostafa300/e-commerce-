namespace IA_E_commerce_.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTeamTableRequest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teams", "MTRequest", c => c.Boolean(nullable: false));
            AddColumn("dbo.Teams", "MTLRequest", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Teams", "MTLRequest");
            DropColumn("dbo.Teams", "MTRequest");
        }
    }
}
