<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewContactLogs.ascx.cs" Inherits="CWSStart.Web.usercontrols.Dashboard.ViewContactLogs" %>

<!-- CSS File -->
<link href="/css/CWS-log-viewer.css" rel="stylesheet" />

<div id="log-viewer">
    <h2>Contact Logs</h2>
    <asp:Repeater runat="server" ID="contactLogs">
        <HeaderTemplate>
            <table>
                <thead>
                    <th>ID</th>
                    <th>Date</th>
                    <th>Name</th>
                    <th>Email</th>
                    <th>Message</th>
                </thead>
        </HeaderTemplate>
        <ItemTemplate>
            <tbody>
                <tr>
                    <td><%# Eval("Id") %></td>
                    <td><%# Eval("Date") %></td>
                    <td><%# Eval("Name") %></td>
                    <td><%# Eval("Email") %></td>
                    <td><%# Eval("Message") %></td>
                </tr>
            </tbody>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</div>