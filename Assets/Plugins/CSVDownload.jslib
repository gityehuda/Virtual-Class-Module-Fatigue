mergeInto(LibraryManager.library, {
    DownloadFile: function (filename, text) {
        var element = document.createElement('a');
        element.setAttribute('href', 'data:text/csv;charset=utf-8,' + encodeURIComponent(UTF8ToString(text)));
        element.setAttribute('download', UTF8ToString(filename));

        element.style.display = 'none';
        document.body.appendChild(element);

        element.click();

        document.body.removeChild(element);
    }
});