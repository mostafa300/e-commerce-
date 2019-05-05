namespace IA_E_commerce_.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTeamTable : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AspNetUsers", t => t.IdentUser_Id)
                .ForeignKey("dbo.PostContents", t => t.post_Id)
                .Index(t => t.IdentUser_Id)
                .Index(t => t.post_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teams", "post_Id", "dbo.PostContents");
            DropForeignKey("dbo.Teams", "IdentUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Teams", new[] { "post_Id" });
            DropIndex("dbo.Teams", new[] { "IdentUser_Id" });
            DropTable("dbo.Teams");
        }
    }
}
