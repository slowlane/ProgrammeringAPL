// project-create.js - Hanterar dynamisk tilläggning och borttagning av projektavsnitt
$(document).ready(function () {

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