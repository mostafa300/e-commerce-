namespace IA_E_commerce_.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCutomerRelationPost : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PostContents", "Relationid", c => c.String());
            DropColumn("dbo.AspNetUsers", "Relationid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Relationid", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.PostContents", "Relationid", c => c.Int(nullable: false));
        }
    }
}
