namespace IA_E_commerce_.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProjectID : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Teams", "Project_Id", "dbo.PostContents");
            DropIndex("dbo.Teams", new[] { "Project_Id" });
            RenameColumn(table: "dbo.Teams", name: "Project_Id", newName: "ProjectID");
            AlterColumn("dbo.Teams", "ProjectID", c => c.Int(nullable: false));
            CreateIndex("dbo.Teams", "ProjectID");
            AddForeignKey("dbo.Teams", "ProjectID", "dbo.PostContents", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teams", "ProjectID", "dbo.PostContents");
            DropIndex("dbo.Teams", new[] { "ProjectID" });
            AlterColumn("dbo.Teams", "ProjectID", c => c.Int());
            RenameColumn(table: "dbo.Teams", name: "ProjectID", newName: "Project_Id");
            CreateIndex("dbo.Teams", "Project_Id");
            AddForeignKey("dbo.Teams", "Project_Id", "dbo.PostContents", "Id");
        }
    }
}
