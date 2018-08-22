using System.Activities;

namespace CRM.Workshops.Workflows
{
    public class AddActivity : CodeActivity
    {
        public InArgument<int> FirstSummand { get; set; }
        public InArgument<int> SecondSummand { get; set; }

        public OutArgument<int> Result { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}