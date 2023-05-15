namespace Genshin_Trade_Center.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "Resource_Id", "dbo.Resources");
            DropIndex("dbo.AspNetUsers", new[] { "Resource_Id" });
            CreateTable(
                "dbo.ResourceUsers",
                c => new
                    {
                        Resource_Id = c.Int(nullable: false),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Resource_Id, t.User_Id })
                .ForeignKey("dbo.Resources", t => t.Resource_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Resource_Id)
                .Index(t => t.User_Id);
            
            DropColumn("dbo.AspNetUsers", "Resource_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Resource_Id", c => c.Int());
            DropForeignKey("dbo.ResourceUsers", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ResourceUsers", "Resource_Id", "dbo.Resources");
            DropIndex("dbo.ResourceUsers", new[] { "User_Id" });
            DropIndex("dbo.ResourceUsers", new[] { "Resource_Id" });
            DropTable("dbo.ResourceUsers");
            CreateIndex("dbo.AspNetUsers", "Resource_Id");
            AddForeignKey("dbo.AspNetUsers", "Resource_Id", "dbo.Resources", "Id");
        }
    }
}
