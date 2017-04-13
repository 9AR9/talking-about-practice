using System.Collections.Generic;
using NUnit.Framework;

namespace TalkingAboutPractice.PatternsAndSolutions.InteractiveOrgChart
{
    [TestFixture]
    public class InteractiveOrgChart
    {
        /*
         * Interactive Org Chart problem
         * 
         * You're tasked with implementing a web application which provides a user interface to view
         * and edit org charts. The following constraints should be supported:
         *  - A given employee can have multiple direct reports.
         *  - A given employee can report only to one person.
         *  - The sequence in which direct reports show up is important
         *  - Your application should be able to handle very large organizations (thousands of
         *    employees) with deep hierarchies (10 or more reporting levels).
         *  
         *  Your solution should address the following components:
         *   1. Define the domain model and database schema to represent the org chart.
         *   2. Describe the algorithm to retrieve the org chart and present it to the user for editing.
         *   3. Describe the algorithm to update the org chart both in the UI and the database
         *      whenever a user changes an employee assignment.
         *      
         *  NOTE: Refer to image interactive-org-chart-problem.jpg in this solution folder for a visual of 
         *        a sample org chart before and after of an edit to the org chart.
        */

        public class Employee
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public LinkedList<int> Reports { get; set; }
            public int BossId { get; set; }
        }

        public class Report
        {
            public int Id { get; set; }
            public int BossId { get; set; }
            public int ReportId { get; set; }
            public int Order { get; set; }

            /* Constraints for this data table:
             *   - Id FK to Employee.Id
             *   - Uniqueness constraint on BossId, ReportId (no two rows should contain these same values)
            */
        }
        


        public void MoveEmployeeWithinOrgChart(int movingEmployeeId, int newBossId, int newPreviousReportId, int oldBossId)
        {
            /* PSEUDO CODE:
             * Employee emp = getEmployee(movingEmployeeId);
             * emp.BossId = newBossId;
             * EmployeeService.UpdateEmployee(emp);
             * 
             * Employee newBoss = getEmployee(newBossId);
             * var previousReport = newBoss.Reports.Find(newPreviousReportId);
             * newBoss.Reports.AddAfter(previousReport, movingEmployeeId);
             * EmployeeService.AddNewReportTo(newBoss, movingEmployeeId);
             *          - Add new row to Reports table (newBossId, movingEmployeeId, 0 for Order on initial insert)
             *          - Cycle through newBoss.Reports, updating each database row with new Order value (index of location in LinkedList + 1)
             *          
             * Employee oldBoss = getEmployee(oldBossId);
             * var reportToRemove = oldBoss.Reports.Find(movingEmployeeId);
             * oldBoss.Reports.Remove(movingEmployeeId);
             * EmployeeService.RemoveReportFrom(oldBoss, movingEmployeeId);
             *          - Remove row from Reports table (where BossId = oldBossId and ReportId = movingEmployeeId)
             *          - Cycle through oldBoss.Reports, updating each database row with new Order value (index of location in LinkedList + 1)
            */
        }

        public void DrawOrgChart()
        {
            /*
             * PSUEDO CODE:
             * var bigBossMan = EmployeeService.GetFirstEmployeeWithNoBoss(); 
             * DrawEmployee(bigBossMan);
             * 
            */
        }

        public void DrawEmployee(Employee emp)
        {
            /*
             * PSUEDO CODE:
             * DrawEmployeeBox(emp, emp.BossId);
             * foreach(report in emp.Reports)
             *          - DrawEmployee(report) // Recursive call
             * 
            */
        }

        [Test]
        public void Testage()
        {

        }
    }
}