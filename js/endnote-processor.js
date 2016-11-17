function attachExportChangeListener(node) {
    node.addEventListener("click", function (evt) {
        if (evt.target.value != 'E') {
            var exp = document.getElementById('export-' + evt.target.parentNode.parentNode.id);
            if (exp) {
                exp.innerHTML = evt.target.value + ' ' + evt.target.parentNode.parentNode.childNodes[1].outerHTML;
            } else {
                var newy = document.createElement("p");
                newy.id = 'export-' + evt.target.parentNode.parentNode.id;
                newy.innerHTML = evt.target.value + ' ' + evt.target.parentNode.parentNode.childNodes[1].outerHTML;
                document.getElementById("exports").appendChild(newy);
            }
        } else {
            var exp = document.getElementById('export-' + evt.target.parentNode.parentNode.id);
            if (exp) {
                exp.parentNode.removeChild(exp);
            }
        }
    }, false);
}
function handleFileSelect(evt) {
    var files = evt.target.files;
    var reader = new FileReader();
    window._endnoteIdCounter = 0;
    get_endnotes(files[0]).then(function (r) {
        var selector = document.createElement("span");
        selector.innerHTML += '<input type="radio" name="type" value="B">Book</input>';
        selector.innerHTML += '<input type="radio" name="type" value="C">Case</input>';
        selector.innerHTML += '<input type="radio" name="type" value="J">Journal</input>';
        selector.innerHTML += '<input type="radio" name="type" value="L">Legislative</input>';
        selector.innerHTML += '<input type="radio" name="type" value="P">Periodical</input>';
        selector.innerHTML += '<input type="radio" name="type" value="M">Miscellaneous</input>';
        selector.innerHTML += '<input type="radio" name="type" value="E">Exclude</input>';

        r.childNodes.forEach(function (q) { 
            var line = document.createElement("p");
            line.id = 'line-' + window._endnoteIdCounter++;
            var newSelector = selector.cloneNode(true);
            var inputs = newSelector.getElementsByTagName('input');
            for (var i = 0; i < inputs.length; i++) {
                attachExportChangeListener(inputs[i]);
            }
            line.appendChild(newSelector);
            line.appendChild(q);
            document.getElementById("endnotes").appendChild(line);
        });
    });
}
document.getElementById("import_docx_file").addEventListener("change", handleFileSelect, false);
