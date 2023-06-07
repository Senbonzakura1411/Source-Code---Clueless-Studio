#include "TopDownCharacter.h"

ATopDownCharacter::ATopDownCharacter()
{
	PrimaryActorTick.bCanEverTick = true;
	MovementSpeed = 250.0f; // Set the desired movement speed
}

void ATopDownCharacter::SetupPlayerInputComponent(UInputComponent* PlayerInputComponent)
{
	Super::SetupPlayerInputComponent(PlayerInputComponent);

	// Bind the input axis events to the character controller functions
	PlayerInputComponent->BindAxis("MoveForward", this, &ATopDownCharacter::MoveForward);
	PlayerInputComponent->BindAxis("MoveRight", this, &ATopDownCharacter::MoveRight);
}

void ATopDownCharacter::BeginPlay()
{
	Super::BeginPlay();
}


void ATopDownCharacter::Tick(float DeltaTime)
{
	Super::Tick(DeltaTime);

	// Move the character based on the current velocity
	FVector NewLocation = GetActorLocation() + (CurrentVelocity * DeltaTime);
	SetActorLocation(NewLocation);
}

void ATopDownCharacter::MoveForward(float Value)
{
	CurrentVelocity.X = FMath::Clamp(Value, -1.0f, 1.0f) * MovementSpeed;
}

void ATopDownCharacter::MoveRight(float Value)
{
	CurrentVelocity.Y = FMath::Clamp(Value, -1.0f, 1.0f) * MovementSpeed;
}
