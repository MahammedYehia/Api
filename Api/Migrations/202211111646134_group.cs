namespace Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class group : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        GroupId = c.Int(nullable: false, identity: true),
                        GroupName = c.String(nullable: false),
                        Term = c.String(nullable: false),
                        Educationallevel = c.String(nullable: false),
                        FirstDate = c.DateTime(nullable: false),
                        SecondDate = c.DateTime(nullable: false),
                        ThirdDate = c.DateTime(nullable: false),
                        FourthDate = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GroupId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Groups", "UserId", "dbo.Users");
            DropIndex("dbo.Groups", new[] { "UserId" });
            DropTable("dbo.Groups");
        }
    }
}
