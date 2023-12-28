namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tb_Intro",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 150),
                        Alias = c.String(),
                        Description = c.String(),
                        Detail = c.String(),
                        Image = c.String(),
                        IntroCategoryId = c.Int(nullable: false),
                        SeoTitle = c.String(),
                        SeoDescription = c.String(),
                        SeoKeywords = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        Modifiedby = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tb_Review",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Username = c.String(),
                        FullName = c.String(),
                        Email = c.String(),
                        Content = c.String(),
                        Rate = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        Avatar = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tb_Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.tb_Wishlist",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        UserName = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tb_Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            AddColumn("dbo.tb_Order", "CustomerId", c => c.String());
            AddColumn("dbo.tb_Product", "Author", c => c.String(nullable: false, maxLength: 250));
            AddColumn("dbo.tb_Product", "NXB", c => c.String(nullable: false, maxLength: 250));
            AddColumn("dbo.tb_Product", "GioiThieu", c => c.String());
            AddColumn("dbo.tb_Product", "AudioGioiThieu", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tb_Wishlist", "ProductId", "dbo.tb_Product");
            DropForeignKey("dbo.tb_Review", "ProductId", "dbo.tb_Product");
            DropIndex("dbo.tb_Wishlist", new[] { "ProductId" });
            DropIndex("dbo.tb_Review", new[] { "ProductId" });
            DropColumn("dbo.tb_Product", "AudioGioiThieu");
            DropColumn("dbo.tb_Product", "GioiThieu");
            DropColumn("dbo.tb_Product", "NXB");
            DropColumn("dbo.tb_Product", "Author");
            DropColumn("dbo.tb_Order", "CustomerId");
            DropTable("dbo.tb_Wishlist");
            DropTable("dbo.tb_Review");
            DropTable("dbo.tb_Intro");
        }
    }
}
