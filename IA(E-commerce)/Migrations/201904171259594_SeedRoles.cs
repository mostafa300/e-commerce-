namespace IA_E_commerce_.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedRoles : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO AspNetRoles (Id , Name) VALUES (1,'Admin')");
            Sql("INSERT INTO AspNetRoles (Id , Name) VALUES (2,'MD')");
            Sql("INSERT INTO AspNetRoles (Id , Name) VALUES (3,'MTL')");
            Sql("INSERT INTO AspNetRoles (Id , Name) VALUES (4,'MT')");
            Sql("INSERT INTO AspNetRoles (Id , Name) VALUES (5,'Customer')");
        }
        
        public override void Down()
        {
        }
    }
}
