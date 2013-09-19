/// <reference path="typings/jquery/jquery.d.ts"/>
/// <reference path="typings/jqueryui/jqueryui.d.ts"/>

$(() => {
    var submitFunction = function () {
        var ajaxForm = $(this);
        var targetSelector = ajaxForm.attr("data-blog-ajax-target");

        var jsonOptions = ((form) => {
            var action = form.attr("action");
            var type = form.attr("method");
            var data = form.serialize();

            return {
                url: action,
                type: type,
                data: data
            };
        })(ajaxForm);

        $.ajax(jsonOptions).done((replaceSelector =>
            response => {
                var replacement = $(response);
                $(replaceSelector)
                    .replaceWith(replacement);
                replacement
                    .effect("highlight");
            }
         )(targetSelector));

        return false;
    };

    var createAutocomplete = function () {
        var input = $(this);
        input.autocomplete({
            source: input.attr("data-blog-autocomplete"),
            select: function (event, ui){
                var input = $(this);
                input.val(ui.item.label);

                var parentForm = input.parents("form:first");
                parentForm.submit();
            }
        });
    };
    $("form[data-blog-ajax='true']").submit(submitFunction);
    $("input[data-blog-autocomplete]").each(createAutocomplete);
});