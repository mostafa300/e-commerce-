namespace IA_E_commerce_.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTableFeedbackActor1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Teams", new[] { "Project_Id" });
            RenameColumn(table: "dbo.FeedbackEvaluates", name: "Actor_Id", newName: "MDID");
            RenameIndex(table: "dbo.FeedbackEvaluates", name: "IX_Actor_Id", newName: "IX_MDID");
            AddColumn("dbo.FeedbackEvaluates", "MTID", c => c.String(maxLength: 128));
            AddColumn("dbo.FeedbackEvaluates", "MTLID", c => c.String(maxLength: 128));
            AlterColumn("dbo.Teams", "Project_Id", c => c.Int());
            AlterColumn("dbo.Teams", "Project_id", c => c.Int(nullable: false));
            CreateIndex("dbo.FeedbackEvaluates", "MTID");
            CreateIndex("dbo.FeedbackEvaluates", "MTLID");
            CreateIndex("dbo.Teams", "Project_Id");
            AddForeignKey("dbo.FeedbackEvaluates", "MTID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.FeedbackEvaluates", "MTLID", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.FeedbackEvaluates", "ActorMDID");
            DropColumn("dbo.FeedbackEvaluates", "ActorMTLID");
            DropColumn("dbo.FeedbackEvaluates", "ActorMTID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FeedbackEvaluates", "ActorMTID", c => c.String());
            AddColumn("dbo.FeedbackEvaluates", "ActorMTLID", c => c.String());
            AddColumn("dbo.FeedbackEvaluates", "ActorMDID", c => c.String());
            DropForeignKey("dbo.FeedbackEvaluates", "MTLID", "dbo.AspNetUsers");
            DropForeignKey("dbo.FeedbackEvaluates", "MTID", "dbo.AspNetUsers");
            DropIndex("dbo.Teams", new[] { "Project_Id" });
            DropIndex("dbo.FeedbackEvaluates", new[] { "MTLID" });
            DropIndex("dbo.FeedbackEvaluates", new[] { "MTID" });
            AlterColumn("dbo.Teams", "Project_id", c => c.Int());
            AlterColumn("dbo.Teams", "Project_Id", c => c.Int(nullable: false));
            DropColumn("dbo.FeedbackEvaluates", "MTLID");
            DropColumn("dbo.FeedbackEvaluates", "MTID");
            RenameIndex(table: "dbo.FeedbackEvaluates", name: "IX_MDID", newName: "IX_Actor_Id");
            RenameColumn(table: "dbo.FeedbackEvaluates", name: "MDID", newName: "Actor_Id");
            CreateIndex("dbo.Teams", "Project_Id");
        }
    }
}
