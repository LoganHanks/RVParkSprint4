document.addEventListener('DOMContentLoaded', function () {
    bindInlineEditToggle();
    bindDeleteButtons();
});

function bindInlineEditToggle() {
    document.querySelectorAll('.edit-cell-btn').forEach(function (button) {
        button.addEventListener('click', function () {
            var cell = this.closest('.editable-cell');
            cell.querySelector('.cell-display').style.display = 'none';
            var editForm = cell.querySelector('.cell-edit-form');
            editForm.style.display = 'flex';
            editForm.querySelector('input').focus();
        });
    });

    document.querySelectorAll('.cell-cancel').forEach(function (button) {
        button.addEventListener('click', function () {
            var cell = this.closest('.editable-cell');
            cell.querySelector('.cell-edit-form').style.display = 'none';
            cell.querySelector('.cell-display').style.display = '';
        });
    });
}

function bindDeleteButtons() {
    document.querySelectorAll('.delete-btn').forEach(function (button) {
        button.addEventListener('click', function (event) {
            if (!confirm('Are you sure you want to delete this price entry?')) {
                event.preventDefault();
            }
        });
    });
}
