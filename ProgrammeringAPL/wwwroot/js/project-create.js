
$(document).ready(function () {
    //// Bindar Add Technology-knappen med .on() så att dynamiska element också fungerar
    //$(document).on('click', '#add-technology', function () {
    //    var index = $('#technologies .technology-item').length;
    //    var template = `
    //                <div class="technology-item">
    //                    <input name="Technologies[` + index + `].Name" class="form-control" placeholder="Technology Name" />
    //                    <span class="text-danger field-validation-valid" data-valmsg-for="Technologies[` + index + `].Name" data-valmsg-replace="true"></span>
    //                    <button type="button" class="btn btn-danger remove-technology">Remove</button>
    //                </div>`;
    //    $('#technologies').append(template);
    //});

    //// Dynamisk borttagning av teknologi
    //$(document).on('click', '.remove-technology', function () {
    //    $(this).parent().remove();
    //});

    //// Bindar Add Tag-knappen med .on() så att dynamiska element också fungerar
    //$(document).on('click', '#add-tag', function () {
    //    var index = $('#tags .tag-item').length;
    //    var template = `
    //                <div class="tag-item">
    //                    <input name="Tags[` + index + `].Name" class="form-control" placeholder="Tag Name" />
    //                    <span class="text-danger field-validation-valid" data-valmsg-for="Tags[` + index + `].Name" data-valmsg-replace="true"></span>
    //                    <button type="button" class="btn btn-danger remove-tag">Remove</button>
    //                </div>`;
    //    $('#tags').append(template);
    //});

    //// Dynamisk borttagning av taggar
    //$(document).on('click', '.remove-tag', function () {
    //    $(this).parent().remove();
    //});
     // lägg till ett nytt teknologiavsnitt dynamiskt när "add tech" knappen klickas
     $('#add-technology').click(function () {
         var index = $('#technologies .technology-item').length;
         var template = `
                     <div class="technology-item">
                         <input name="technologies[` + index + `].name" class="form-control" placeholder="technology name" />
                         <span class="text-danger field-validation-valid" data-valmsg-for="technologies[` + index + `].name" data-valmsg-replace="true"></span>
                         <button type="button" class="btn btn-danger remove-technology">remove</button>
                     </div>`;
         $('#technologies').append(template);
     });
     // tar bort en specifik teknologi när "remove" knappen klickas
     $(document).on('click', '.remove-technology', function () {
         $(this).parent().remove();

     });

     // lägg till en ny tag-avsnitt dynamiskt när "add tag" knappen klickas
     $('#add-tag').click(function () {
         var index = $('#tags .tag-item').length;
         var template = `
                     <div class="tag-item">
                         <input name="tags[` + index + `].name" class="form-control" placeholder="tag name" />
                         <span class="text-danger field-validation-valid" data-valmsg-for="tags[` + index + `].name" data-valmsg-replace="true"></span>
                         <button type="button" class="btn btn-danger remove-tag">remove</button>
                     </div>`;
         $('#tags').append(template);
     });
     // tar bort en specifik tag när "remove" knappen klickas
     $(document).on('click', '.remove-tag', function () {
         $(this).parent().remove();
     });

     //lägg till ett nytt galleriavsnitt dynamiskt när "add image" knappen klickas, med en knapp för att ta bort avsnittet
    $('#add-gallery').click(function () {
        var index = $('#gallery .gallery-item').length;
        var template = `
                <div class="gallery-item">
                    <label>Image File</label>
                    <input name="Gallery[` + index + `].ImageFile" type="file" class="form-control" />
                    <span class="text-danger field-validation-valid" data-valmsg-for="Gallery[` + index + `].ImageFile" data-valmsg-replace="true"></span>

                    <label>Caption</label>
                    <input name="Gallery[` + index + `].Caption" class="form-control" placeholder="Caption" />

                    <button type="button" class="btn btn-danger remove-gallery">Remove</button>
                </div>`;
        $('#gallery').append(template);

        // Tar bort en specifik bild när "Remove" knappen klickas

    });
    $(document).on('click', '.remove-gallery', function () {
        $(this).parent().remove();

    });
});