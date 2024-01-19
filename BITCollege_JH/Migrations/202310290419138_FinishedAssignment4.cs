namespace BITCollege_JH.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FinishedAssignment4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Courses", "CourseNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Courses", "CourseNumber", c => c.String(nullable: false));
        }
    }
}
