(function () {
    window.QuillFunctions = {
        createQuill: function (quillElement, toolBar, readOnly, placeholder, theme, debugLevel, dotNetRef) {
            var options = {
                debug: debugLevel && debugLevel.length > 0 ? debugLevel : false,
                placeholder: placeholder || "",
                readOnly: !!readOnly,
                theme: theme || "snow",
                modules: {}
            };

            if (toolBar) {
                options.modules.toolbar = toolBar;
            } else {
                options.modules.toolbar = true;
            }

            var quill = new Quill(quillElement, options);
            quillElement.__quill = quill;

            quill.on('text-change', function (delta, oldDelta, source) {
                if (source === 'user') {
                    var html = quill.root.innerHTML;
                    dotNetRef.invokeMethodAsync('OnContentChanged', html);
                }
            });
        },

        setQuillHtml: function (quillElement, html) {
            var quill = quillElement.__quill;
            if (!quill) return;

            if (!html) {
                quill.setText('', 'api');
                return;
            }

            quill.clipboard.dangerouslyPasteHTML(html, 'api');
        },

        getQuillHTML: function (quillElement) {
            var quill = quillElement.__quill;
            if (!quill) return "";
            return quill.root.innerHTML;
        },

        enableQuillEditor: function (quillElement, mode) {
            var quill = quillElement.__quill;
            if (!quill) return;
            quill.enable(!!mode);
        }
    };
})();
