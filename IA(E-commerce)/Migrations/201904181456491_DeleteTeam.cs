namespace IA_E_commerce_.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteTeam : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Teams", "IdentUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Teams", "post_Id", "dbo.PostContents");
            DropIndex("dbo.Teams", new[] { "IdentUser_Id" });
            DropIndex("dbo.Teams", new[] { "post_Id" });
            DropTable("dbo.Teams");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 128),
                        MTLid = c.String(),
                        MTid = c.String(),
                        Project_id = c.Int(nullable: false),
                        IdentUser_Id = c.String(maxLength: 128),
                        post_Id = c.Int(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateIndex("dbo.Teams", "post_Id");
            CreateIndex("dbo.Teams", "IdentUser_Id");
            AddForeignKey("dbo.Teams", "post_Id", "dbo.PostContents", "Id");
            AddForeignKey("dbo.Teams", "IdentUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
