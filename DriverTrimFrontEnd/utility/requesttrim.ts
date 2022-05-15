


function requesttrim(access_token, start_date, end_date, callback){
    let headers = {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
    }


    fetch ("drivetrim.tmanti.dev/api/trim",{
        access_token: access_token,

        start_date: start_date = {
            "day": 0, "month": 0, "year": 0
        },

        end_date: end_date = {
            "day": 0, "month": 0, "year": 0
        }

    }).then((ret)=> ret.json()).then(callback).catch(function(error){
        console.log(error.message);
    });
    
}



export {requesttrim};