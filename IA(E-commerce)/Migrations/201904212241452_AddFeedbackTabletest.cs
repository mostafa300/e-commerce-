namespace IA_E_commerce_.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFeedbackTabletest : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FeedbackEvaluates", "FeedProject_Id", "dbo.PostContents");
            DropIndex("dbo.FeedbackEvaluates", new[] { "FeedProject_Id" });
            DropIndex("dbo.Teams", new[] { "Project_Id" });
            RenameColumn(table: "dbo.FeedbackEvaluates", name: "FeedProject_Id", newName: "FeedProjectID");
            AlterColumn("dbo.FeedbackEvaluates", "FeedProjectID", c => c.Int(nullable: false));
            AlterColumn("dbo.Teams", "Project_Id", c => c.Int());
            AlterColumn("dbo.Teams", "Project_id", c => c.Int(nullable: false));
            CreateIndex("dbo.FeedbackEvaluates", "FeedProjectID");
            CreateIndex("dbo.Teams", "Project_Id");
            AddForeignKey("dbo.FeedbackEvaluates", "FeedProjectID", "dbo.PostContents", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FeedbackEvaluates", "FeedProjectID", "dbo.PostContents");
            DropIndex("dbo.Teams", new[] { "Project_Id" });
            DropIndex("dbo.FeedbackEvaluates", new[] { "FeedProjectID" });
            AlterColumn("dbo.Teams", "Project_id", c => c.Int());
            AlterColumn("dbo.Teams", "Project_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.FeedbackEvaluates", "FeedProjectID", c => c.Int());
            RenameColumn(table: "dbo.FeedbackEvaluates", name: "FeedProjectID", newName: "FeedProject_Id");
            CreateIndex("dbo.Teams", "Project_Id");
            CreateIndex("dbo.FeedbackEvaluates", "FeedProject_Id");
            AddForeignKey("dbo.FeedbackEvaluates", "FeedProject_Id", "dbo.PostContents", "Id");
        }
    }
}
