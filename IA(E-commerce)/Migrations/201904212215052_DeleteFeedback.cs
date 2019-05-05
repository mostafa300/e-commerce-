namespace IA_E_commerce_.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteFeedback : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Feedbacks", "Actor_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Feedbacks", "FeedPost_Id", "dbo.PostContents");
            DropIndex("dbo.Feedbacks", new[] { "Actor_Id" });
            DropIndex("dbo.Feedbacks", new[] { "FeedPost_Id" });
            DropIndex("dbo.Teams", new[] { "Project_Id" });
            AlterColumn("dbo.Teams", "Project_Id", c => c.Int());
            AlterColumn("dbo.Teams", "Project_id", c => c.Int(nullable: false));
            CreateIndex("dbo.Teams", "Project_Id");
            DropTable("dbo.Feedbacks");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Rate = c.String(),
                        Actor_MDID = c.String(),
                        Actor_MTLID = c.String(),
                        Actor_MTID = c.String(),
                        FeedPost_id = c.Int(nullable: false),
                        Actor_Id = c.String(maxLength: 128),
                        FeedPost_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropIndex("dbo.Teams", new[] { "Project_Id" });
            AlterColumn("dbo.Teams", "Project_id", c => c.Int());
            AlterColumn("dbo.Teams", "Project_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Teams", "Project_Id");
            CreateIndex("dbo.Feedbacks", "FeedPost_Id");
            CreateIndex("dbo.Feedbacks", "Actor_Id");
            AddForeignKey("dbo.Feedbacks", "FeedPost_Id", "dbo.PostContents", "Id");
            AddForeignKey("dbo.Feedbacks", "Actor_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
