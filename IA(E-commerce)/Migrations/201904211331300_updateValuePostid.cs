namespace IA_E_commerce_.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateValuePostid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Feedbacks", "FeedPost_id", c => c.Int(nullable: false));
            CreateIndex("dbo.Feedbacks", "FeedPost_Id");
            AddForeignKey("dbo.Feedbacks", "FeedPost_Id", "dbo.PostContents", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Feedbacks", "FeedPost_Id", "dbo.PostContents");
            DropIndex("dbo.Feedbacks", new[] { "FeedPost_Id" });
            DropColumn("dbo.Feedbacks", "FeedPost_id");
        }
    }
}
