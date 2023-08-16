// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function clearQuery() {
    document.getElementById("searchQuery").value = "";
    document.getElementById("searchForm").submit(); // 提交表單
}
