using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.VersionControl.Client;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TfsCore
{
    public class TfsService
    {
        private readonly TfsConnection ivConnection;

        public TfsService(TfsConnection connection)
        {
            ivConnection = connection;
        }
        public List<Branch> GetBranches(string branchStartsWith = null)
        {
            VersionControlServer versionServer = GetVersionServer();
            BranchObject[] branchObjects = versionServer.QueryRootBranchObjects(RecursionType.OneLevel);

            var branches = new List<Branch>();
            foreach (BranchObject branch in branchObjects)
            {
                PoupulateBranches(branch, versionServer, branches, branchStartsWith);
            }
            return branches;
        }

        public List<CheckInDetail> GetCheckInDetails(CheckInRequestParam requestParam)
        {
            VersionControlServer versionServer = GetVersionServer();

            var changesets = versionServer.QueryHistory(requestParam.Branch.Name, VersionSpec.Latest, 0, RecursionType.Full, "", null, null, int.MaxValue, false, false);
            var checkInDetails = new List<CheckInDetail>();
            foreach (Changeset changeset in changesets)
            {
                if (changeset.CreationDate < requestParam.StartDate || changeset.CreationDate > requestParam.EndDate) continue;
                var checkInDetail = new CheckInDetail()
                {
                    ChangesetId = changeset.ChangesetId,
                    Date = changeset.CreationDate,
                    Comment = changeset.Comment,
                    Owner = changeset.OwnerDisplayName
                };
                AssociatedWorkItemInfo[] linkedItems = changeset.AssociatedWorkItems;
                foreach (AssociatedWorkItemInfo wi in linkedItems)
                {
                    checkInDetail.AssociatedWorkItems.Add(new AssociatedWorkItem()
                    {
                        Id = wi.Id,
                        Title = wi.Title,
                        Type = wi.WorkItemType,
                        AssignedTo = wi.AssignedTo,
                        State = wi.State
                    });
                }
                checkInDetails.Add(checkInDetail);
            }
            return checkInDetails.OrderByDescending(c => c.Date).ToList();
        }
        private void PoupulateBranches(BranchObject branch, VersionControlServer versionServer, List<Branch> outBranches, string branchStartsWith)
        {
            if (!string.IsNullOrWhiteSpace(branchStartsWith))
            {
                if (branch.Properties.RootItem.Item.StartsWith(branchStartsWith))
                    outBranches.Add(new Branch() { Name = branch.Properties.RootItem.Item });
            }
            else
            {
                outBranches.Add(new Branch() { Name = branch.Properties.RootItem.Item });
            }

            var childBos = versionServer.QueryBranchObjects(branch.Properties.RootItem, RecursionType.OneLevel);
            foreach (var child in childBos)
            {
                if (child.Properties.RootItem.Item == branch.Properties.RootItem.Item)
                    continue;

                PoupulateBranches(child, versionServer, outBranches, branchStartsWith);
            }

        }

        private VersionControlServer GetVersionServer()
        {
            TfsTeamProjectCollection tfs = new TfsTeamProjectCollection(new Uri(ivConnection.CollectionPath));
            VersionControlServer vcs = tfs.GetService<VersionControlServer>();
            return vcs;
        }

    }
}
