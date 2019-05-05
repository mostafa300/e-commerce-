namespace IA_E_commerce_.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSchecduleToPost : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Teams", new[] { "Project_Id" });
            AddColumn("dbo.PostContents", "StartingPoint", c => c.DateTime());
            AddColumn("dbo.PostContents", "EndingPoint", c => c.DateTime());
            AddColumn("dbo.PostContents", "Price", c => c.Single(nullable: true));
            AlterColumn("dbo.PostContents", "delivered", c => c.Boolean());
            AlterColumn("dbo.PostContents", "assign", c => c.Boolean());
            AlterColumn("dbo.PostContents", "MDRequest", c => c.Boolean());
            AlterColumn("dbo.PostContents", "Approvement", c => c.Boolean());
            AlterColumn("dbo.Teams", "Project_Id", c => c.Int());
            AlterColumn("dbo.Teams", "Project_id", c => c.Int(nullable: false));
            CreateIndex("dbo.Teams", "Project_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Teams", new[] { "Project_Id" });
            AlterColumn("dbo.Teams", "Project_id", c => c.Int());
            AlterColumn("dbo.Teams", "Project_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.PostContents", "Approvement", c => c.Boolean(nullable: false));
            AlterColumn("dbo.PostContents", "MDRequest", c => c.Boolean(nullable: false));
            AlterColumn("dbo.PostContents", "assign", c => c.Boolean(nullable: false));
            AlterColumn("dbo.PostContents", "delivered", c => c.Boolean(nullable: false));
            DropColumn("dbo.PostContents", "Price");
            DropColumn("dbo.PostContents", "EndingPoint");
            DropColumn("dbo.PostContents", "StartingPoint");
            CreateIndex("dbo.Teams", "Project_Id");
        }
    }
}
