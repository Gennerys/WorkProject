document.addEventListener('DOMContentLoaded', () => {

    let items = [];
    let toDosItems = [];
    const title = document.getElementById('title');
    const description = document.getElementById('description');
    const type = document.getElementById('workItemType');
    const workItems = document.getElementById("workItems");
    const toDoText = document.getElementById('todo');
    const toDos = document.getElementById('ToDos')
    loadDataFromStorage();

    document.getElementById('addWorkItem').addEventListener('click', createWorkItem);
    document.getElementById('addToDo').addEventListener('click',createToDo);

    function loadDataFromStorage() {
        items = JSON.parse( localStorage.getItem('items'));
        for(let i = 0; i < items.length; i++){
            const li = document.createElement("li");
            li.appendChild(document.createTextNode(items[i].title + " " +items[i].description + " " + items[i].type));
            workItems.appendChild(li);
        }
    }

    document.addEventListener('DOMContentLoaded',loadDataFromStorage);

    function createWorkItem() {
        const workItem = getWorkItemData();
        const li = document.createElement("li");
        li.appendChild(document.createTextNode(workItem.title + " " + workItem.description + " " + workItem.type));
        workItems.appendChild(li);
        saveWorkItem(workItem);
    }

    function getWorkItemData() {
        return {
            title: title.value,
            description: description.value,
            type: type.value
        };
    }

    function saveWorkItem(item) {
        items.push(item);
        localStorage.setItem('items', JSON.stringify(items));
        console.log(items.length);
        items.forEach(function (item, index, array) {
            console.log(item, index);
        });
    }

    function getToDoData(){
        return {
            toDoText: toDoText.value
        };
    }

    function createToDo(){
        const toDo = getToDoData();
        const li = document.createElement("li");
        li.appendChild(document.createTextNode(toDo.toDoText))
        toDos.appendChild(li);
    }

    // function createUUID() {
    //     let dateTime = new Date().getTime();
    //     let uuid = 'xxxxxxxx-xxxx-xxxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
    //         let r = (dateTime + Math.random() * 16) % 16 | 0;
    //         dateTime = Math.floor(dateTime / 16);
    //         return (c == 'x' ? r : (r & 0x3 | 0x8)).toString(16);
    //     });
    //
    //     return uuid;
    // }


    function onClickHandler(event) {
        console.log(event.target.id);

    }
});



