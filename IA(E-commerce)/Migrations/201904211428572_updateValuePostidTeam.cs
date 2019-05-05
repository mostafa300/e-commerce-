namespace IA_E_commerce_.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateValuePostidTeam : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Feedbacks", new[] { "FeedPost_Id" });
            DropIndex("dbo.Teams", new[] { "post_Id" });
            DropColumn("dbo.Teams", "Project_id");
            RenameColumn(table: "dbo.Teams", name: "post_Id", newName: "Project_Id");
            AlterColumn("dbo.Feedbacks", "FeedPost_Id", c => c.Int());
            AlterColumn("dbo.Feedbacks", "FeedPost_id", c => c.Int(nullable: false));
            AlterColumn("dbo.Teams", "Project_Id", c => c.Int());
            AlterColumn("dbo.Teams", "Project_id", c => c.Int(nullable: false));
            CreateIndex("dbo.Feedbacks", "FeedPost_Id");
            CreateIndex("dbo.Teams", "Project_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Teams", new[] { "Project_Id" });
            DropIndex("dbo.Feedbacks", new[] { "FeedPost_Id" });
            AlterColumn("dbo.Teams", "Project_id", c => c.Int());
            AlterColumn("dbo.Teams", "Project_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Feedbacks", "FeedPost_id", c => c.Int());
            AlterColumn("dbo.Feedbacks", "FeedPost_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Teams", name: "Project_Id", newName: "post_Id");
            AddColumn("dbo.Teams", "Project_id", c => c.Int(nullable: false));
            CreateIndex("dbo.Teams", "post_Id");
            CreateIndex("dbo.Feedbacks", "FeedPost_Id");
        }
    }
}
