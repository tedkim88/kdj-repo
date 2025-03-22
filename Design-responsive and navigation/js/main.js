document.querySelector('.hamburger').addEventListener('click',()=>{
    
    const collapsed = document.querySelectorAll('.collapsed');
    
    collapsed.forEach(item=>{
        item.classList.toggle('expanded');
    })

})


document.querySelector('.toggle').addEventListener('click',()=>{
    document.querySelector('.dropdown').classList.toggle('expanded');
})