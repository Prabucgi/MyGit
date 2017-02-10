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

        public void LinkChangeSets(int workItem, List<int> changeSetIds)
        {
            //var server = new TeamFoundationServer(serverName);
            //var workItemStore = (WorkItemStore)server.GetService(typeof(WorkItemStore));
            //var project = workItemStore.Projects[projectName];
            //VersionControlServer versionServer = GetVersionServer();
            //var userStoryId = 9;
            //var changeSetsIds = new[] { 2, 3, 4, 5 };

            //var userStory = versionServer.pro.Store.GetWorkItem(userStoryId);

            //foreach (var changeSetId in changeSetsIds)
            //{
            //    var changeSetLink = new RelatedLink(changeSetId);
            //    userStory.Links.Add(changeSetLink);
            //}

            //userStory.Save();
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

        public void MergeChangeSets(string piSourceBranch, string piTargetBranch, List<int> piChangesetIds)
        {
            // Get a reference to yourTeam Foundation Server. 
            //  TfsTeamProjectCollection tpc = new TfsTeamProjectCollection(new Uri("http://<yourserver>:8080/tfs/<yourcollection> "));

            // Get a reference to Version Control. 
            //  VersionControlServer versionControl = tpc.GetService<VersionControlServer>();
            VersionControlServer versionServer = GetVersionServer();
            Workspace workspace = versionServer.GetWorkspace(@"C:\Users\chendurpandianp\Code\Dev2013");

            piChangesetIds.ForEach(cs =>
            {
                VersionSpec changesetToMerge = new ChangesetVersionSpec(cs);

                // actually pend the merge
                GetStatus status = workspace.Merge(piSourceBranch, piTargetBranch, changesetToMerge, changesetToMerge);
                if (status.NumConflicts > 0)
                {
                    // workspace.MergeContent(status.)
                }

                // check in the merge
                workspace.CheckIn(workspace.GetPendingChanges(), "Platform merge");
            });

        }
    }
}
