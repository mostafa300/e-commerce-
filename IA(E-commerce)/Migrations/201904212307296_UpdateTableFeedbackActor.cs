namespace IA_E_commerce_.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTableFeedbackActor : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Teams", new[] { "Project_Id" });
            AddColumn("dbo.FeedbackEvaluates", "ActorMDID", c => c.String());
            AddColumn("dbo.FeedbackEvaluates", "ActorMTLID", c => c.String());
            AddColumn("dbo.FeedbackEvaluates", "ActorMTID", c => c.String());
            AlterColumn("dbo.Teams", "Project_Id", c => c.Int());
            AlterColumn("dbo.Teams", "Project_id", c => c.Int(nullable: false));
            CreateIndex("dbo.Teams", "Project_Id");
            DropColumn("dbo.FeedbackEvaluates", "Actor_MDID");
            DropColumn("dbo.FeedbackEvaluates", "Actor_MTLID");
            DropColumn("dbo.FeedbackEvaluates", "Actor_MTID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FeedbackEvaluates", "Actor_MTID", c => c.String());
            AddColumn("dbo.FeedbackEvaluates", "Actor_MTLID", c => c.String());
            AddColumn("dbo.FeedbackEvaluates", "Actor_MDID", c => c.String());
            DropIndex("dbo.Teams", new[] { "Project_Id" });
            AlterColumn("dbo.Teams", "Project_id", c => c.Int());
            AlterColumn("dbo.Teams", "Project_Id", c => c.Int(nullable: false));
            DropColumn("dbo.FeedbackEvaluates", "ActorMTID");
            DropColumn("dbo.FeedbackEvaluates", "ActorMTLID");
            DropColumn("dbo.FeedbackEvaluates", "ActorMDID");
            CreateIndex("dbo.Teams", "Project_Id");
        }
    }
}
