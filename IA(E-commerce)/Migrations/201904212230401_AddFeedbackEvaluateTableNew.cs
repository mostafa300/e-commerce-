namespace IA_E_commerce_.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFeedbackEvaluateTableNew : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Teams", new[] { "Project_Id" });
            CreateTable(
                "dbo.FeedbackEvaluates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Rate = c.String(),
                        Actor_MDID = c.String(),
                        Actor_MTLID = c.String(),
                        Actor_MTID = c.String(),
                        FeedProject_id = c.Int(nullable: false),
                        Actor_Id = c.String(maxLength: 128),
                        FeedPost_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Actor_Id)
                .ForeignKey("dbo.PostContents", t => t.FeedPost_Id)
                .Index(t => t.Actor_Id)
                .Index(t => t.FeedPost_Id);
            
            AlterColumn("dbo.Teams", "Project_Id", c => c.Int());
            AlterColumn("dbo.Teams", "Project_id", c => c.Int(nullable: false));
            CreateIndex("dbo.Teams", "Project_Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FeedbackEvaluates", "FeedPost_Id", "dbo.PostContents");
            DropForeignKey("dbo.FeedbackEvaluates", "Actor_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Teams", new[] { "Project_Id" });
            DropIndex("dbo.FeedbackEvaluates", new[] { "FeedPost_Id" });
            DropIndex("dbo.FeedbackEvaluates", new[] { "Actor_Id" });
            AlterColumn("dbo.Teams", "Project_id", c => c.Int());
            AlterColumn("dbo.Teams", "Project_Id", c => c.Int(nullable: false));
            DropTable("dbo.FeedbackEvaluates");
            CreateIndex("dbo.Teams", "Project_Id");
        }
    }
}
