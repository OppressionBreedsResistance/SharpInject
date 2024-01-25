# SharpInject
Wstrzykiwanie shellcode do bieżącego procesu

# Program wykorzystuje reflective function loading i wykorzystuje tylko trzy importy
- LoadLibrary
- GetProcAddress
- Sleep

## AV Evasion
- Reflective function usage
- Encrypting shellcode

## Sandbox Evasion
- Simple sleep antidebugger

## Jeżeli interesuje Cię wstrzykiwanie shellcode do innego procesu niż bieżący - zobacz repozytorium SharpInjectProcess
