namespace IA_E_commerce_.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRelationid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PostContents", "Relationid", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "Relationid", c => c.Int(nullable: false));
            DropColumn("dbo.PostContents", "Userid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PostContents", "Userid", c => c.String());
            DropColumn("dbo.AspNetUsers", "Relationid");
            DropColumn("dbo.PostContents", "Relationid");
        }
    }
}
