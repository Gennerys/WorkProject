document.addEventListener('DOMContentLoaded', () => {

    let items = [];
    // let toDoItems = [];
    const title = document.getElementById('title');
    const description = document.getElementById('description');
    const type = document.getElementById('workItemType');
    const workItems = document.getElementById("workItems");
    const toDoText = document.getElementById('todo');
    const toDos = document.getElementById('ToDos');
    const toDoState = document.getElementById('toDoState');

    loadDataFromStorage();

    document.getElementById('addWorkItem').addEventListener('click', createWorkItem);
    document.getElementById('addToDo').addEventListener('click',createToDo);

    function loadDataFromStorage() {
        console.log(items);
        items = JSON.parse( localStorage.getItem('items'));
        console.log(items);
        if(items == null){
            return items = [];
        }
        for(let i = 0; i < items.length; i++){
            const item = items[i];
            const li = document.createElement("li");
            li.appendChild(document.createTextNode(item.title + " " +item.description + " " + item.type));
            li.addEventListener('click', selectWorkItem(item));
            workItems.appendChild(li);
        }
    }

    function selectWorkItem(item) {
        return function() {
            console.log(item);
        };
    }



    function createWorkItem() {
        const workItem = getWorkItemData();
        const li = document.createElement("li");
        li.appendChild(document.createTextNode(workItem.title + " " + workItem.description + " " + workItem.type));
        li.addEventListener('click',selectWorkItem(workItem));
        workItems.appendChild(li);
        saveWorkItem(workItem);
    }

    function getWorkItemData() {
        return {
            title: title.value,
            description: description.value,
            type: type.value,
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

    function createToDo(){
        const toDo = getToDoData();
        const li = document.createElement("li");
        if(toDo.toDoState === "Created"){
            li.setAttribute('Status','Created');
        }
        else{
            li.style.textDecoration = 'line-through';
            li.appendChild(document.createTextNode(toDo.toDoText));
            li.setAttribute('Status','Done');
        }
        toDos.appendChild(li);
        saveToDo(toDo);
        createDeleteButtonForTodo(li);
        createCompletedButtonForTodo(li);
    }

    function saveToDo(toDo) {
        toDoItems.push(toDo);
        localStorage.setItem('toDos',JSON.stringify(toDoItems));
    }
    function createDeleteButtonForTodo(li) {
        let deleteToDo = document.createElement('Button');
        let deleteButtonText = document.createTextNode('Delete ToDo');
        deleteToDo.appendChild(deleteButtonText);
        li.appendChild(deleteToDo);
        deleteToDo.addEventListener('click', ()=>{
            toDoItems.splice(toDoItems.indexOf(li),1);
            toDos.removeChild(li);
        });
    }

    function createCompletedButtonForTodo(li) {
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



    function onClickHandler(event) {
        console.log(event.target.id);
    }
});



