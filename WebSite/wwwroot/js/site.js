
function clickImage() {
    let id = $(this).data('id');
    if(id)
        window.location.href = `/home/image/${id}`;
}

function deleteImage() {
    let id = $(this).data('id');
    let btn = $(this);

    $.ajax({
        url: `/api/image/${id}`,
        type: 'DELETE',
        success: function(result) {
            btn.remove();
            let img = $(`.img-square[data-id="${id}"`);
            img.remove();
            alert('Deleted!');
        }
    });
}

function showDelete(_, img) {
    let id = $(this).data('id');

    let btn = $(`<i class="far fa-trash-alt delete-btn" data-id="${id}"></i>`).
        click('click', deleteImage).
        insertAfter($(img));
}

function uploadFile() {
    let fd = new FormData();
    let file = $('#upload')[0].files[0];
    fd.append('file', file);

    $.ajax({
        url: `/api/image`,
        type: 'POST',
        data: fd,
        contentType: false,
        processData: false,
        success: function(data) {
            let img = $(`<div class="img-square" style="background-image: url('${data.url}')" data-id="${data.id}" data-comment-number="${data.commentNumber}" data-like-number="${data.likeNumber}"></div>`).
                click(clickImage).
                insertBefore($('.img-container').first());
        }
    });
}

function sendComment() {

    $.ajax({
        url: `/api/comment`,
        type: 'POST',
        data: {
            text: $('#comment').val(),
            id: $('#image-id').val()
        },
        success: function(data) {
            $('#comment').val('');
            $('.comment-list-box ul').append(
            `<li class="comment"><p class="comment-text">${data.text}</p><p class="comment-date">${data.date}</p></li>`);
        }
    });
}

function like() {
    let btn = $(this);
    const id = $('#image-id').val();

    if(btn.hasClass('liked'))
    {
        $.ajax({
            url: `/api/like?imageId=${id}`,
            type: 'DELETE',
            success: function(result) {
                btn.toggleClass('liked');
            }
        });
    }
    else
    {
        $.ajax({
            url: `/api/like`,
            type: 'POST',
            data: {
                imageId: id
            },
            success: function(result) {
                btn.toggleClass('liked');
            }
        });
    }
}

$(document).ready(function() {
    $('.img-square').click(clickImage);
    $('.admin .img-square').each(showDelete);

    $('#upload').on('change', uploadFile);

    $('#send-comment').click(sendComment);
    $('#like').click(like);
});