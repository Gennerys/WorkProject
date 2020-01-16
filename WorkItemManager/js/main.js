document.addEventListener('DOMContentLoaded', () => {

    let items = [];
    const title = document.getElementById('title');
    const description = document.getElementById('description');
    const type = document.getElementById('workItemType');
    const workItems = document.getElementById("workItems");
    const toDoText = document.getElementById('todo');
    const toDos = document.getElementById('ToDos');
    const toDoState = document.getElementById('toDoState');
    const addToDoButton = document.getElementById('addToDo');
    let selectedWorkItem = null;
    loadDataFromStorage();

    document.getElementById('addWorkItem').addEventListener('click', createWorkItem);
    addToDoButton.addEventListener('click',createToDo);

    function loadDataFromStorage() {
        console.log(items);
        items = JSON.parse( localStorage.getItem('items'));
        console.log(items);
        if(items == null){
            return items = [];
        }
        for(let i = 0; i < items.length; i++){
            renderWorkItems(items[i]);
        }
    }

    function createWorkItem() {
        const workItem = getWorkItemData();
        renderWorkItems(workItem);
        saveWorkItem(workItem);
    }
    function renderWorkItems(item) {
        const li = document.createElement("li");
        li.appendChild(document.createTextNode(item.title + " " + item.description + " " + item.type));
        li.setAttribute('id',`${item.id}`);
        li.addEventListener('click',onClickWorkItem);
        workItems.appendChild(li);
    }

    function getWorkItemData() {
        return {
            title: title.value,
            description: description.value,
            type: type.value,
            id: (Math.random() * 10000000).toFixed(0),
            toDoList: []
        };
    }

    function saveWorkItem(workItem) {
        console.log(typeof (items));
        items.push(workItem);
        localStorage.setItem('items', JSON.stringify(items));
        console.log(items.length);
        items.forEach(function (workItem, index, array) {
            console.log(workItem, index);
        });
    }

    function getToDoData(){
        return {
            toDoText: toDoText.value,
            toDoState: toDoState.value
        };
    }

    function createDeleteButtonForTodo(li,selectedWorkItem) {
        let deleteToDo = document.createElement('Button');
        let deleteButtonText = document.createTextNode('Delete ToDo');
        deleteToDo.appendChild(deleteButtonText);
        li.appendChild(deleteToDo);
        deleteToDo.addEventListener('click', ()=>{
            console.log(toDosItems);
            selectedWorkItem.splice(selectedWorkItem.indexOf(li),1);
            toDos.removeChild(li);
            localStorage.setItem('items', JSON.stringify(items));
        });
    }

    function createCompletedButtonForTodo(li) {
        console.log(li);
        let completeToDo = document.createElement('Button');
        let completeButtonText;
        if(li.getAttribute('Status') === "Created"){
             completeButtonText = document.createTextNode('Complete ToDo');
        }
        else{
            completeButtonText = document.createTextNode('Incomplete ToDo');
        }
        completeToDo.appendChild(completeButtonText);
        li.appendChild(completeToDo);
        completeToDo.addEventListener('click', ()=>{
         if(li.getAttribute('Status') === "Created"){
             li.style.textDecoration = 'line-through';
             li.setAttribute('Status','Done');
             completeToDo.innerHTML = 'Incomplete ToDo';
         }
         else{
             li.style.textDecoration ='none';
             li.setAttribute('Status','Created');
             completeToDo.innerHTML = 'Complete ToDo';
         }
        });
    }

    function saveToDo(currentWorkItemToDoList,toDo) {
        currentWorkItemToDoList.push(toDo);
        localStorage.setItem('items', JSON.stringify(items));
    }

    function retrieveWorkItemToDoList(event) {
         items = JSON.parse(localStorage.getItem('items'));
         let currentWorkItemToDoList = items.find(element => element.id === event.target.id).toDoList;
         selectedWorkItem = currentWorkItemToDoList;

         return currentWorkItemToDoList;
    }
    function createToDo() {
        const toDo = getToDoData();
        renderToDoItem(toDo);
        saveToDo(selectedWorkItem,toDo);
        return toDo;
    }
    function renderToDoItem(toDo) {
        const li = document.createElement("li");
        if(toDo.toDoState === "Created"){
            li.appendChild(document.createTextNode(toDo.toDoText));
            li.setAttribute('Status','Created');
        }
        else{
            li.style.textDecoration = 'line-through';
            li.appendChild(document.createTextNode(toDo.toDoText));
            li.setAttribute('Status','Done');
        }
        toDos.appendChild(li);
        createCompletedButtonForTodo(li);
        createDeleteButtonForTodo(li,selectedWorkItem);
    }

    function renderToDoItems(currentWorkItemToDoList) {
        clearToDoItemList();
        for(let i = 0; i < currentWorkItemToDoList.length; i++){
            renderToDoItem(currentWorkItemToDoList[i]);
        }
    }
    function clearToDoItemList() {
        toDos.innerHTML = '';
    }

   function onClickWorkItem(event) {
        addToDoButton.disabled = false;
        let currentWorkItemToDoList = retrieveWorkItemToDoList(event);
        console.log(currentWorkItemToDoList);
        renderToDoItems(currentWorkItemToDoList);
    }

});



