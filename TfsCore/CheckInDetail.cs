using System;
using System.Collections.Generic;
using System.Linq;

namespace TfsCore
{
    public class CheckInDetail
    {
        public CheckInDetail()
        {
            AssociatedWorkItems = new List<AssociatedWorkItem>();
        }
        public int ChangesetId { get; set; }
        public DateTime Date { get; set; }
        public string Owner { get; set; }
        public string Comment { get; set; }
        public List<AssociatedWorkItem> AssociatedWorkItems { get; set; }

        public int AssociatedItemsCount
        {
            get { return AssociatedWorkItems.Count(wi => !wi.Type.Equals("Code Review Request")); }
        }

        public int CodeReviewCount
        {
            get { return AssociatedWorkItems.Count(wi => wi.Type.Equals("Code Review Request")); }
        }

        public int UserStoryCount
        {
            get { return AssociatedWorkItems.Count(wi => wi.Type.Equals("User Story")); }
        }
    }
}
