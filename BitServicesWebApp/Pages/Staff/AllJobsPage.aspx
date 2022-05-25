<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AllJobsPage.aspx.cs" Inherits="BitServicesWebApp.Pages.Staff.AllJobsPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <script>
            function toggleFilter(btnElement) {
                btnElement.classList.toggle("filter-active");
                const cbName = btnElement.id.replace("lbtn", "cb");
                const comboBox = document.getElementById(cbName);
                comboBox.checked = !comboBox.checked;
            }
            function clearFilters() {
                const buttons = document.querySelectorAll('.modal-body .btn');
                buttons.forEach(button => {
                    button.classList.remove('filter-active');
                });

                const checkBoxes = document.querySelectorAll('.checkbox input');
                checkBoxes.forEach(checkBox => {
                    checkBox.active = false;
                });
            }
            function setButtonStyle()
            {
                const checkBoxes = document.querySelectorAll('.checkbox input');
                checkBoxes.forEach(checkBox => {
                    if (checkBox.checked) {
                        const button = document.querySelector('#' + checkBox.id.replace("cb", "lbtn"));
                        button.classList.add("filter-active");
                    }
                });
            }
        </script>
        <!-- Modal -->
        <div class="modal fade" id="filterModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
            <div class="modal-dialog modal-lg modal-dialog-centered" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLongTitle">Filters</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true"><i class="fa-solid fa-xmark"></i></span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <p class="strong mb-1" style="font-size: 1.1rem">
                            Job Status
                        </p>
                        <p class="small">Pick which jobs you see by filtering the jobs by status. If no statues are selected you will be shown all jobs.</p>
                        <div class="row">
                            <div class="col">
                                <asp:LinkButton runat="server" OnClientClick="toggleFilter(this); return false;" CssClass="btn btn-filter d-flex flex-column" ID="lbtnPending">
                                    <i class="fa-solid fa-people-arrows-left-right mb-4 h1"></i>
                                        <span>Pending jobs</span>
                                </asp:LinkButton>
                                <asp:CheckBox runat="server" CssClass="checkbox d-none" ID="cbPending"/>
                            </div>
                            <div class="col">
                                <asp:LinkButton runat="server" OnClientClick="toggleFilter(this); return false;" CssClass="btn btn-filter d-flex flex-column" ID="lbtnIn_Progress">
                                        <i class="fa-solid fa-person-digging mb-4 h1"></i>
                                        <span>In Progress jobs</span>
                                </asp:LinkButton>
                                <asp:CheckBox runat="server" CssClass="checkbox d-none" ID="cbIn_Progress"/>
                            </div>
                            <div class="col">
                                <asp:LinkButton runat="server" OnClientClick="toggleFilter(this); return false;" CssClass="btn btn-filter d-flex flex-column" ID="lbtnCompleted">
                                    <i class="fa-solid fa-person-burst mb-4 h1"></i>
                                        <span>Completed jobs</span>
                                </asp:LinkButton>
                                <asp:CheckBox runat="server" CssClass="checkbox d-none" ID="cbCompleted"/>
                            </div>
                            <div class="col">
                                <asp:LinkButton runat="server" OnClientClick="toggleFilter(this); return false;" CssClass="btn btn-filter d-flex flex-column" ID="lbtnVerified">
                                        
                                         <i class="fa-solid fa-person-circle-check mb-4 h1"></i>
                                        <span>Verified jobs</span>
                                </asp:LinkButton>
                                <asp:CheckBox runat="server" CssClass="checkbox d-none" ID="cbVerified"/>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <asp:LinkButton runat="server" OnClientClick="toggleFilter(this); return false;" CssClass="btn btn-filter d-flex flex-column" ID="lbtnRejected">
                                        <i class="fa-solid fa-person-circle-xmark mb-4 h1"></i>
                                        <span>Rejected jobs</span>
                                </asp:LinkButton>
                                <asp:CheckBox runat="server" CssClass="checkbox d-none" ID="cbRejected"/>
                            </div>
                            <div class="col">
                                <asp:LinkButton runat="server" OnClientClick="toggleFilter(this); return false;" CssClass="btn btn-filter d-flex flex-column" ID="lbtnCanceled">
                                        <i class="fa-solid fa-person-circle-minus mb-4 h1"></i>
                                        <span>Canceled jobs</span>
                                </asp:LinkButton>
                                <asp:CheckBox runat="server" CssClass="checkbox d-none" ID="cbCanceled"/>
                            </div>
                            <div class="col"></div>
                            <div class="col"></div>
                        </div>
                    </div>

                    <div class="row mx-0 modal-footer">
                        <div class="col text-left">
                            <asp:LinkButton runat="server" CssClass="btn btn-link text-left" ID="lbtnClearFilters" OnClientClick="clearFilters(); return false;">
                            Clear all
                            </asp:LinkButton>
                        </div>
                        <div class="col text-right">
                            <asp:LinkButton runat="server" class="btn btn-secondary" ID="lbtnApplyFilters" OnClick="lbtnApplyFilters_Click">
                            Apply changes
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-12">
                <div class="row mb-2">
                    <div class="col align-middle">
                        <h3>All jobs</h3>
                    </div>
                    <%-- TODO: add filter to filter jobs via the status (in progress, pending, completed, verified, canceled) and rejected jobs. Rejected jobs will be shown in a seperate GridView --%>
                    <div class="col text-right align-middle">
                        <asp:LinkButton CssClass="btn btn-outline d-none" ID="lbtnAssignContractors"
                            runat="server" OnClick="lbtnAssignContractors_OnClick">
                            <asp:Label runat="server" ID="txtAssignContractors"></asp:Label>
                            <asp:Label runat="server" ID="lblAssignContractors" CssClass="new"></asp:Label>
                        </asp:LinkButton>
                        <asp:LinkButton CssClass="btn btn-outline ml-2 d-none" ID="lbtnVerifyJobs"
                            runat="server" OnClick="lbtnVerifyJobs_OnClick">
                            <asp:Label runat="server" ID="txtVerifyJobs"></asp:Label>
                            <asp:Label runat="server" ID="lblVerifyJobs" CssClass="new"></asp:Label>
                        </asp:LinkButton>
                        <!-- Button trigger modal -->
                        <asp:LinkButton OnClick="lbtnFilter_OnClick" CssClass="btn btn-primary ml-2" ID="lbtnFilter" runat="server" data-toggle="modal" data-target="#filterModal">
                            <i class="fa-solid fa-sliders mr-2"></i> Filter
                        </asp:LinkButton>
                    </div>
                </div>
                <div class="row mb-4">
                    <div class="col-12 mx-auto">
                        <asp:Panel runat="server" CssClass="text-center mt-5 d-none" ID="pnlNoJobs">
                            <i class="fa-solid fa-building-circle-exclamation h1 mb-3"></i>
                            <p class="">There are currently no jobs in the system.</p>
                        </asp:Panel>
                        <asp:GridView ID="gvAllJobs"
                            CssClass="table table-striped table-bordered" AutoGenerateColumns="false"
                            runat="server" DataKeyNames="job_id">
                            <Columns>
                                <asp:BoundField DataField="Status" HeaderText="Status" />
                                <asp:BoundField DataField="Client Location" HeaderText="Client Location" />
                                <asp:BoundField DataField="Contractor Name" HeaderText="Contractor Name" />
                                <asp:BoundField DataField="Required Skill" HeaderText="Required Skill" />
                                <asp:BoundField DataField="Title" HeaderText="Title" />
                                <asp:BoundField DataField="Description" HeaderText="Description" />
                                <asp:BoundField DataField="Deadline Date" HeaderText="Deadline Date" />
                                <asp:BoundField DataField="Completion Date" HeaderText="Completion Date" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <h3 class="mb-3">Rejected jobs</h3>
                <div class="row">
                    <div class="col-12 mx-auto">
                        <asp:Panel runat="server" CssClass="text-center mt-5 d-none" ID="pnlNoRejectedJobs">
                            <i class="fa-solid fa-building-circle-exclamation h1 mb-3"></i>
                            <p class="">There are currently no rejected jobs in the system.</p>
                        </asp:Panel>
                        <asp:GridView ID="gvRejectedJobs"
                                      CssClass="table table-striped table-bordered" AutoGenerateColumns="false"
                                      runat="server" DataKeyNames="job_id">
                            <Columns>
                                <asp:BoundField DataField="Client Location" HeaderText="Client Location" />
                                <asp:BoundField DataField="Contractor Name" HeaderText="Contractor Name" />
                                <asp:BoundField DataField="Assigned By" HeaderText="Assigned By" />
                                <asp:BoundField DataField="Required Skill" HeaderText="Required Skill" />
                                <asp:BoundField DataField="Title" HeaderText="Job Title" />
                                <asp:BoundField DataField="Description" HeaderText="Job Description" />
                                <asp:BoundField DataField="Deadline Date" HeaderText="Job Deadline Date" />
                                <asp:BoundField DataField="Rejection Reason" HeaderText="Rejection Reason" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
