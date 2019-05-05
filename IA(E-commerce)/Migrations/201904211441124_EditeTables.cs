namespace IA_E_commerce_.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditeTables : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Feedbacks", new[] { "FeedPost_Id" });
            DropIndex("dbo.Teams", new[] { "Project_Id" });
            AddColumn("dbo.Teams", "MTLid", c => c.String());
            AddColumn("dbo.Teams", "MTid", c => c.String());
            AlterColumn("dbo.Feedbacks", "FeedPost_Id", c => c.Int());
            AlterColumn("dbo.Feedbacks", "FeedPost_id", c => c.Int(nullable: false));
            AlterColumn("dbo.Teams", "Project_Id", c => c.Int());
            AlterColumn("dbo.Teams", "Project_id", c => c.Int(nullable: false));
            CreateIndex("dbo.Feedbacks", "FeedPost_Id");
            CreateIndex("dbo.Teams", "Project_Id");
            DropColumn("dbo.Teams", "IdentUser_MTLid");
            DropColumn("dbo.Teams", "IdentUser_MTid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Teams", "IdentUser_MTid", c => c.String());
            AddColumn("dbo.Teams", "IdentUser_MTLid", c => c.String());
            DropIndex("dbo.Teams", new[] { "Project_Id" });
            DropIndex("dbo.Feedbacks", new[] { "FeedPost_Id" });
            AlterColumn("dbo.Teams", "Project_id", c => c.Int());
            AlterColumn("dbo.Teams", "Project_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Feedbacks", "FeedPost_id", c => c.Int());
            AlterColumn("dbo.Feedbacks", "FeedPost_Id", c => c.Int(nullable: false));
            DropColumn("dbo.Teams", "MTid");
            DropColumn("dbo.Teams", "MTLid");
            CreateIndex("dbo.Teams", "Project_Id");
            CreateIndex("dbo.Feedbacks", "FeedPost_Id");
        }
    }
}
