document.addEventListener('DOMContentLoaded', function () {
    const taskList = document.getElementById('task-list');
    const newTaskInput = document.getElementById('new-task');
    const addTaskButton = document.getElementById('add-task');

    addTaskButton.addEventListener('click', function () {
        const taskText = newTaskInput.value.trim();
        if (taskText) {
            const listItem = document.createElement('li');
            listItem.innerHTML = `
                <span>${taskText}</span>
                <button class="delete">Eliminar</button>
            `;
            taskList.appendChild(listItem);
            newTaskInput.value = '';
        }
    });

    taskList.addEventListener('click', function (event) {
        if (event.target.classList.contains('delete')) {
            event.target.parentElement.remove();
        }
    });
});
