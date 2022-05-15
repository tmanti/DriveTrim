interface date {
    day:Number,
    month:Number,
    year:Number
}

function requesttrim(access_token:string, start_date:any, end_date:any, callback:any){
    let headers = {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
    }

    let body = {
        access_token: access_token,

        start_date: start_date,

        end_date: end_date,
    }

    fetch ("drivetrim.tmanti.dev/api/trim",{
        method:"POST",
        headers:headers,
        body:JSON.stringify(body)
    }).then((ret)=> ret.json()).then(callback).catch(function(error){
        console.log(error.message);
    });
    
}



export {requesttrim};