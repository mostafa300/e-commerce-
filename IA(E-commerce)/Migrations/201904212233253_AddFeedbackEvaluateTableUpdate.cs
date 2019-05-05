namespace IA_E_commerce_.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFeedbackEvaluateTableUpdate : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.FeedbackEvaluates", new[] { "FeedPost_Id" });
            DropIndex("dbo.Teams", new[] { "Project_Id" });
            DropColumn("dbo.FeedbackEvaluates", "FeedProject_id");
            RenameColumn(table: "dbo.FeedbackEvaluates", name: "FeedPost_Id", newName: "FeedProject_Id");
            AlterColumn("dbo.FeedbackEvaluates", "FeedProject_Id", c => c.Int());
            AlterColumn("dbo.FeedbackEvaluates", "FeedProject_id", c => c.Int(nullable: false));
            AlterColumn("dbo.Teams", "Project_Id", c => c.Int());
            AlterColumn("dbo.Teams", "Project_id", c => c.Int(nullable: false));
            CreateIndex("dbo.FeedbackEvaluates", "FeedProject_Id");
            CreateIndex("dbo.Teams", "Project_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Teams", new[] { "Project_Id" });
            DropIndex("dbo.FeedbackEvaluates", new[] { "FeedProject_Id" });
            AlterColumn("dbo.Teams", "Project_id", c => c.Int());
            AlterColumn("dbo.Teams", "Project_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.FeedbackEvaluates", "FeedProject_id", c => c.Int());
            AlterColumn("dbo.FeedbackEvaluates", "FeedProject_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.FeedbackEvaluates", name: "FeedProject_Id", newName: "FeedPost_Id");
            AddColumn("dbo.FeedbackEvaluates", "FeedProject_id", c => c.Int(nullable: false));
            CreateIndex("dbo.Teams", "Project_Id");
            CreateIndex("dbo.FeedbackEvaluates", "FeedPost_Id");
        }
    }
}
