namespace IA_E_commerce_.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSchecduleToPost2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Teams", new[] { "Project_Id" });
            AlterColumn("dbo.PostContents", "delivered", c => c.Boolean(nullable: false));
            AlterColumn("dbo.PostContents", "assign", c => c.Boolean(nullable: false));
            AlterColumn("dbo.PostContents", "MDRequest", c => c.Boolean(nullable: false));
            AlterColumn("dbo.PostContents", "Approvement", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Teams", "Project_Id", c => c.Int());
            AlterColumn("dbo.Teams", "Project_id", c => c.Int(nullable: false));
            CreateIndex("dbo.Teams", "Project_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Teams", new[] { "Project_Id" });
            AlterColumn("dbo.Teams", "Project_id", c => c.Int());
            AlterColumn("dbo.Teams", "Project_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.PostContents", "Approvement", c => c.Boolean());
            AlterColumn("dbo.PostContents", "MDRequest", c => c.Boolean());
            AlterColumn("dbo.PostContents", "assign", c => c.Boolean());
            AlterColumn("dbo.PostContents", "delivered", c => c.Boolean());
            CreateIndex("dbo.Teams", "Project_Id");
        }
    }
}
