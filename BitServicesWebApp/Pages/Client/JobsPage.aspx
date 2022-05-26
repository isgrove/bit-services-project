<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="JobsPage.aspx.cs" Inherits="BitServicesWebApp.Pages.AllJobsPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="/Scripts/FilterManager.js"></script>
    <div class="container">
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
                    </div>

                    <div class="row mx-0 modal-footer">
                        <div class="col text-left">
                            <asp:LinkButton runat="server" CssClass="btn btn-link text-left" ID="lbtnClearFilters" OnClientClick="clearFilters(); return false;">
                            Clear all
                            </asp:LinkButton>
                        </div>
                        <div class="col text-right">
                            <asp:LinkButton runat="server" class="btn btn-secondary" ID="lbtnApplyFilters" OnClick="lbtnApplyFilters_OnClick">
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
                    <div class="col">
                        <h3>All your jobs</h3>
                    </div>
                    <%-- TODO: add filter to filter jobs via the status (pending, in progress, completed, verified) and rejected jobs. --%>
                    <div class="col text-right">
                        <asp:LinkButton CssClass="btn btn-primary" ID="lbtnNewJob"
                            runat="server" OnClick="lbtnNewJob_OnClick">
                                    New Job
                        </asp:LinkButton>
                        <asp:LinkButton CssClass="btn btn-primary ml-2" ID="lbtnFilter" runat="server" data-toggle="modal" data-target="#filterModal">
                            <i class="fa-solid fa-sliders mr-2"></i> Filter
                        </asp:LinkButton>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12 mx-auto">
                        <asp:Panel runat="server" CssClass="text-center mt-5 d-none" ID="pnlNoJobs">
                            <i class="fa-solid fa-building-circle-exclamation h1 mb-3"></i>
                            <p class="">You have no jobs with us.</p>
                        </asp:Panel>
                        <asp:GridView ID="gvCompletedBookings" AutoGenerateColumns="false"
                            CssClass="table table-striped table-bordered"
                            runat="server">
                            <Columns>
                                <asp:BoundField DataField="Status" HeaderText="Status" />
                                <asp:BoundField DataField="Client Location" HeaderText="Location Suburb" />
                                <asp:BoundField DataField="Required Skill" HeaderText="Job Skill" />
                                <asp:BoundField DataField="Title" HeaderText="Title" />
                                <asp:BoundField DataField="Description" HeaderText="Description" />
                                <asp:BoundField DataField="Deadline Date" HeaderText="Deadline Date" />
                                <asp:BoundField DataField="Completion Date" HeaderText="Completion Date" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
