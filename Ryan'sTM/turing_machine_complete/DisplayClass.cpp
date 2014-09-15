#include "DisplayClass.h"

const string DisplayClass::commands[11] = {"(D)elete","E(x)it","(H)elp","(I)nsert","(L)ist","(Q)uit","(R)un","S(e)t","Sho(w)","(T)runcate","(V)iew"};
const string DisplayClass::command_help[11] = {"Delete input string from list.","Exit application.","Help user with prompts.","Insert input string into list.",
            "List input strings.","Quit operation of Turing Machine on input string.","Run Turing Machine on input string.","Set Maximum number of transitions to perform.",
            "Show status of application.","Truncate instantaneous descriptions.","View Turing machine."};

void DisplayClass::i()
{
    insert();
}

void DisplayClass::d()
{
    _delete();
}

void DisplayClass::t()
{
    truncate();
}

void DisplayClass::e()
{
    set();
}

void DisplayClass::h()
{
    help();
}

void DisplayClass::r_NotOperating()
{
    run_NotOperating();
}
void DisplayClass::r_Operating()
{
	run_Operating();
}

void DisplayClass::q()
{
    quit();
}

void DisplayClass::v()
{
    view();
}

void DisplayClass::p()
{
    prompt();
}
void DisplayClass::_d()
{
    _default();
}


void DisplayClass::_default()
{
    cout<<"\nInvalid command."<<endl;
}
void DisplayClass::prompt()
{
    cout<<"Command: ";
}

void DisplayClass::insert()
{
    cout<<"\nThis is where you enter a string to add to the list.\nInput a string like :aaabbbbb\n"<<endl;
}



void DisplayClass::truncate()
{
  cout<<"\nEnter the max [number] of characters you wish to show in the instantaneous description.\n\n";
}

void DisplayClass::set()
{
    cout<<"\nEnter the max [number] of transitions to perform.\n\n";
}

void DisplayClass::quit()
{
    cout<<"\nThis quits operation of a string if it's running on the Turing Machine.\n\n";
}

void DisplayClass::help()
{

    cout<<endl;
    for(int i = 0;i<11;i++)
    {
        cout.width(25);
        cout<<left<<commands[i]<<right<<command_help[i]<<endl;
    }
    cout<<endl;
}





void DisplayClass::run_NotOperating()
{
    cout<<"\nEnter the string [number] you wish to use for this Turing Machine.\n\n";
}
void DisplayClass::run_Operating()
{
	cout << "\nThis performs the transitions on the string.\n\n";
}

void DisplayClass::view()
{
	cout << "\nThis displays the Turing Machine definition.\n\n";
}
void DisplayClass::_delete()
{
    cout<<"\nEnter the string number from the list you wish to delete.\n\n";
}
