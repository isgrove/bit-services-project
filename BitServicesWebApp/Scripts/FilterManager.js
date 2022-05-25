function toggleFilter(btnElement) {
    btnElement.classList.toggle("filter-active");
    const cbName = btnElement.id.replace("lbtn", "cb");
    const comboBox = document.getElementById(cbName);
    comboBox.checked = !comboBox.checked;
}

function clearFilters() {
    const buttons = document.querySelectorAll('.modal-body .btn');
    buttons.forEach(button => {
        button.classList.remove('filter-active');
    });
    const checkBoxes = document.querySelectorAll('.checkbox input');
    checkBoxes.forEach(checkBox => {
        checkBox.checked = false;
    });
}
function setButtonStyle() {
    const checkBoxes = document.querySelectorAll('.checkbox input');
    checkBoxes.forEach(checkBox => {
        if (checkBox.checked) {
            const button = document.querySelector('#' + checkBox.id.replace("cb", "lbtn"));
            button.classList.add("filter-active");
        }
    });
}