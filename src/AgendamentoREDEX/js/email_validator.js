function mailValidator(email) {
    //validate email            
        var emailParts = emailInput.split('@');
        var text = 'Por favor insira um email válido';

    //at least one @, catches error
    if (emailParts[1] == null || emailParts[1] == "" || emailParts[1] == undefined) {

        //$('#feedback').text(text);
        alert(text);

    } else {

        //split domain, subdomain and tld if existent
        var emailDomainParts = emailParts[1].split('.');

        //at least one . (dot), catches error
        if (emailDomainParts[1] == null || emailDomainParts[1] == "" || emailDomainParts[1] == undefined) {

            //$('#feedback').text(text);
            alert(text);

        } else {

            //more than 2 . (dots) in emailParts[1]
            if (!emailDomainParts[3] == null || !emailDomainParts[3] == "" || !emailDomainParts[3] == undefined) {

                //$('#feedback').text(text);
                alert(text);

            } else {

                //email user
                if (/[^a-z0-9!#$%&'*+-/=?^_`{|}~]/i.test(emailParts[0])) {

                    //$('#feedback').text(text);
                    alert(text);

                } else {

                    //double @
                    if (!emailParts[2] == null || !emailParts[2] == "" || !emailParts[2] == undefined) {

                        //$('#feedback').text(text);
                        alert(text);

                    } else {

                        //domain
                        if (/[^a-z0-9-]/i.test(emailDomainParts[0])) {

                            //$('#feedback').text(text);
                            alert(text);

                        } else {

                            //check for subdomain
                            if (emailDomainParts[2] == null || emailDomainParts[2] == "" || emailDomainParts[2] == undefined) {

                                //TLD
                                if (/[^a-z]/i.test(emailDomainParts[1])) {

                                    //$('#feedback').text(text);
                                    alert(text);

                                } else {

                                    //$('#feedback').text(text);
                                    alert("Email válido");

                                }

                            } else {

                                //subdomain
                                if (/[^a-z0-9-]/i.test(emailDomainParts[1])) {

                                    //$('#feedback').text(text);
                                    alert(text);

                                } else {

                                    //TLD
                                    if (/[^a-z]/i.test(emailDomainParts[2])) {

                                        //$('#feedback').text(text);
                                        alert(text);

                                    } else {

                                        //$('#feedback').text(text);
                                        alert("Email válido");
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

}