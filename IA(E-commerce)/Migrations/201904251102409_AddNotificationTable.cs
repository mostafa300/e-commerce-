namespace IA_E_commerce_.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNotificationTable : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Teams", new[] { "Project_Id" });
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        FromID = c.String(maxLength: 128),
                        ToID = c.String(maxLength: 128),
                        message = c.String(),
                        stutes = c.Boolean(),
                        createdOn = c.DateTime(nullable: false),
                        postID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AspNetUsers", t => t.FromID)
                .ForeignKey("dbo.PostContents", t => t.postID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ToID)
                .Index(t => t.FromID)
                .Index(t => t.ToID)
                .Index(t => t.postID);
            
            AlterColumn("dbo.Teams", "Project_Id", c => c.Int());
            AlterColumn("dbo.Teams", "Project_id", c => c.Int(nullable: false));
            CreateIndex("dbo.Teams", "Project_Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notifications", "ToID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Notifications", "postID", "dbo.PostContents");
            DropForeignKey("dbo.Notifications", "FromID", "dbo.AspNetUsers");
            DropIndex("dbo.Teams", new[] { "Project_Id" });
            DropIndex("dbo.Notifications", new[] { "postID" });
            DropIndex("dbo.Notifications", new[] { "ToID" });
            DropIndex("dbo.Notifications", new[] { "FromID" });
            AlterColumn("dbo.Teams", "Project_id", c => c.Int());
            AlterColumn("dbo.Teams", "Project_Id", c => c.Int(nullable: false));
            DropTable("dbo.Notifications");
            CreateIndex("dbo.Teams", "Project_Id");
        }
    }
}
